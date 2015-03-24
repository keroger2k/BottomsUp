using BottomsUp.Core.Data;
using BottomsUp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BottomsUp.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: Details
        public ActionResult Details(int id)
        {
            return View(new Proposal { Id = id });
        }
    }
}