using System;
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
        [Route("api/requirements/{id}/tasks")]
        public async Task<IHttpActionResult> GetRequirementTasks(int id)
        {
            Requirement requirement = await db.Requirements
                .Include("Tasks")
                .Include("Tasks.Labor")
                .FirstOrDefaultAsync(c => c.Id == id);
            if (requirement == null)
            {
                return NotFound();
            }

            return Ok(requirement.Tasks);
        }

        // PUT: api/Requirements/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRequirement(int id, Requirement requirement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != requirement.Id)
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
                if (!RequirementExists(id))
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
        public async Task<IHttpActionResult> PostRequirement(Requirement requirement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Requirements.Add(requirement);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = requirement.Id }, requirement);
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