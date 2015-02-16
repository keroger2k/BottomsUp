using BottomsUp.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BottomsUp.Web.Controllers
{
    public class DataController : Controller
    {
        protected DatabaseContext _context;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _context = new DatabaseContext();
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _context.SaveChanges();
        }
    }
}