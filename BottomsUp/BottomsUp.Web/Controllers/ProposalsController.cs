using BottomsUp.Core.Data;
using BottomsUp.Core.Models;
using BottomsUp.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BottomsUp.Web.Controllers
{
    public class ProposalsController : DataController
    {
        private readonly IProposalService _propService;
        public ProposalsController(IProposalService propService)
        {
            this._propService = propService;
        }

        public ActionResult Index()
        {
            var proposal = _context.Propsals.Where(c => !c.Deleted).ToList();
            return View(proposal);
        }

        // GET: Proposal/Details/5
        public ActionResult Details(int id)
        {
            var item = _propService.GetProposal(id);
            if (item == null)
                return new HttpNotFoundResult();
            return View(item);
        }

        [HttpGet]
        public ActionResult GetRequirementTasks(int id)
        {
            var item = _propService.GetRequirementTask(id);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        // GET: Proposal/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Proposal/Create
        [HttpPost]
        public ActionResult Create(Proposal proposal)
        {
            if (ModelState.IsValid)
            {
                proposal.Created = DateTime.Now;
                proposal.Updated = DateTime.Now;
                proposal.ModifiedBy = string.IsNullOrEmpty(HttpContext.User.Identity.Name) ? "Unknown" : HttpContext.User.Identity.Name;
                _context.Propsals.Add(proposal);
                return RedirectToAction("Index");
            }
            return View(proposal);
        }

        // GET: Proposal/Edit/5
        public ActionResult Edit(int id)
        {
            var item = _context.Propsals.FirstOrDefault(c => c.Id == id);
            if (item == null)
                return new HttpNotFoundResult();
            return View(item);
        }

        // POST: Proposal/Edit/5
        [HttpPost]
        public ActionResult Edit(Proposal proposal)
        {
            if (ModelState.IsValid)
            {
                var item = _context.Propsals.FirstOrDefault(c => c.Id == proposal.Id);
                if (item == null)
                    return new HttpNotFoundResult();
                item.Updated = DateTime.Now;
                item.ModifiedBy = string.IsNullOrEmpty(HttpContext.User.Identity.Name) ? "Unknown" : HttpContext.User.Identity.Name;
                UpdateModel<Proposal>(item);
                return RedirectToAction("Index");
            }
            return View(proposal);
        }

        // GET: Proposal/Delete/5
        public ActionResult Delete(int id)
        {
            var item = _context.Propsals.FirstOrDefault(c => c.Id == id);
            if (item == null)
                return new HttpNotFoundResult();
            return View(item);
        }

        // POST: Proposal/Delete/5
        [HttpPost]
        public ActionResult Delete(Proposal proposal)
        {
            var item = _context.Propsals.FirstOrDefault(c => c.Id == proposal.Id);
            if (item == null)
                return new HttpNotFoundResult();
            item.Updated = DateTime.Now;
            item.ModifiedBy = string.IsNullOrEmpty(HttpContext.User.Identity.Name) ? "Unknown" : HttpContext.User.Identity.Name;
            item.Deleted = true;
            return RedirectToAction("Index");
        }
    }
}