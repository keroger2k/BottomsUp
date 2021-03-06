﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BottomsUp.Core.Data;
using BottomsUp.Core.Models;

namespace BottomsUp.Api.Controllers
{
    public class RequirementsController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: api/Requirements/5
        [ResponseType(typeof(Requirement))]
        public async Task<IHttpActionResult> GetRequirement(int id)
        {
            Requirement requirement = await db.Requirements.Include("Category").FirstOrDefaultAsync(c => c.Id == id);
            if (requirement == null)
            {
                return NotFound();
            }

            return Ok(requirement);
        }

        // GET: api/Requirements/5
        [ResponseType(typeof(IEnumerable<Tasking>))]
        [Route("api/v1/proposals/{pid}/requirements/{rid}/tasks")]
        public async Task<IHttpActionResult> GetRequirementTasks(int pid, int rid)
        {
            Requirement requirement = await db.Requirements
                .Include("Tasks")
                .Include("Tasks.Labor")
                .FirstOrDefaultAsync(c => c.Id == rid);
            if (requirement == null)
            {
                return NotFound();
            }

            return Ok(requirement.Tasks);
        }

        // PUT: api/Requirements/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRequirement(int pid, Requirement requirement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (pid != requirement.Id)
            {
                return BadRequest();
            }

            db.Entry(requirement).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequirementExists(pid))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Requirements
        [ResponseType(typeof(Requirement))]
        [Route("api/v1/proposals/{pid}/requirements")]
        public async Task<IHttpActionResult> PostRequirement(int pid, Requirement requirement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var proposal = db.Propsals.Find(pid);

            if (proposal == null)
            {
                return BadRequest();
            }

            requirement.Created = DateTime.Now;
            requirement.ModifiedBy = "UNKNOWN";
            requirement.Updated = DateTime.Now;

            proposal.Requirements.Add(requirement);

            await db.SaveChangesAsync();

            return CreatedAtRoute("requirements", new { rid = requirement.Id }, requirement);
        }

        // DELETE: api/Requirements/5
        [ResponseType(typeof(Requirement))]
        public async Task<IHttpActionResult> DeleteRequirement(int id)
        {
            Requirement requirement = await db.Requirements.FindAsync(id);
            if (requirement == null)
            {
                return NotFound();
            }

            db.Requirements.Remove(requirement);
            await db.SaveChangesAsync();

            return Ok(requirement);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RequirementExists(int id)
        {
            return db.Requirements.Count(e => e.Id == id) > 0;
        }
    }
}