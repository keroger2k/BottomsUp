using BottomsUp.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BottomsUp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext db;

        public HomeController()
        {
            this.db = new DatabaseContext();
        }

        public ActionResult Index()
        {
            var proposal = db.Propsals.First();
            return View(proposal);
        }
    }
}