using System.Web.Mvc;
using AzureTableDataStore;
using jutteleminen_sovellus.SignalR;

namespace jutteleminen_sovellus.Controllers
{
    public class HomeController : Controller
    {
        private IChatService _chatService;

        public HomeController(IChatService chatService)
        {
            _chatService = chatService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Chat()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Chat(string firstName, string lastName)
        {
            ViewBag.FirstName = firstName;
            ViewBag.LastName = lastName;
            return View(_chatService.ListUsers());
        }
    }
}
