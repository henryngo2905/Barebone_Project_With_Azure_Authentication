using Azure.Identity;
using Barebone.Logic.UserService;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Barebone.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly GraphService _graphService;

        public HomeController()
        {

        }

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to DevExpress Extensions for ASP.NET MVC!";
            return View();
        }
    }
}