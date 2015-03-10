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

namespace BottomsUp.Web.Controllers
{
    public class TaskingsController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: api/v1/proposals/{pid}/requirements/{rid}/tasks
        [ResponseType(typeof(IEnumerable<Tasking>))]
        public async Task<IHttpActionResult> GetTasks(int pid, int rid)
        {
            var proposal = await db.Requirements.FirstOrDefaultAsync(c => c.Id == rid);
            if (proposal == null)
            {
                return NotFound();
            }

            return Ok(proposal.Tasks);
        }
        
        // PUT: api/Taskings/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTasking(int id, Tasking tasking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tasking.Id)
            {
                return BadRequest();
            }

            db.Entry(tasking).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskingExists(id))
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

        // POST: api/Taskings
        [ResponseType(typeof(Tasking))]
        public async Task<IHttpActionResult> PostTasking(int pid, int rid, Tasking tasking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requirement = db.Requirements.Find(rid);
            if (requirement == null)
            {
                return BadRequest();
            }

            tasking.Updated = DateTime.Now;
            tasking.Created = DateTime.Now;
            tasking.ModifiedBy = "UNKNOWN";
            requirement.Tasks.Add(tasking);
            
            await db.SaveChangesAsync();

            return CreatedAtRoute("tasks", new { tid = tasking.Id }, tasking);
        }

        // DELETE: api/Taskings/5
        [ResponseType(typeof(Tasking))]
        public async Task<IHttpActionResult> DeleteTasking(int id)
        {
            Tasking tasking = await db.Tasks.FindAsync(id);
            if (tasking == null)
            {
                return NotFound();
            }

            db.Tasks.Remove(tasking);
            await db.SaveChangesAsync();

            return Ok(tasking);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TaskingExists(int id)
        {
            return db.Tasks.Count(e => e.Id == id) > 0;
        }
    }
}