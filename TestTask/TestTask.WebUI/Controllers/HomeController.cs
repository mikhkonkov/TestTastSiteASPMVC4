using TestTask.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestTask.WebUI.CustomAttribute;

namespace TestTask.WebUI.Controllers {
    public class HomeController : Controller {
        private IUserRepository repository;

        public HomeController(IUserRepository repo) {
            repository = repo;
        }


        public ActionResult Index() {
            return View();
        }

        [PageAuthorize]
        public ActionResult About() {
            return View();
        }

    }
}
