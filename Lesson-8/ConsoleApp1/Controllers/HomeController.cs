using Client.Service;
using Microsoft.AspNetCore.Mvc;
using Orders.DAL;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBuyerRepository _buyerRepository;

        public HomeController(IBuyerRepository buyerRepository)
        {
            _buyerRepository = buyerRepository;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Главная страница";

            return View(_buyerRepository.GetAll());
        }
    }
}
