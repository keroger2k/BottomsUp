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
    public class ProposalsController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: api/Proposals
        public IQueryable<Proposal> GetPropsals()
        {
            return db.Propsals;
        }

        // GET: api/Proposals/5
        [ResponseType(typeof(Proposal))]
        public async Task<IHttpActionResult> GetProposal(int id)
        {
            Proposal proposal = await db.Propsals.FindAsync(id);
            if (proposal == null)
            {
                return NotFound();
            }

            return Ok(proposal);
        }

        // GET: api/Proposals/5
        [ResponseType(typeof(Proposal))]
        [Route("api/proposals/{id}/requirements")]
        public async Task<IHttpActionResult> GetProposalRequirements(int id)
        {
            Proposal proposal = await db.Propsals
                .Include("Requirements")
                .Include("Requirements.Category")
                .Include("Requirements.Tasks")
                .Include("Requirements.Tasks.Labor")
                .FirstOrDefaultAsync(p => p.Id == id);
            if (proposal == null)
            {
                return NotFound();
            }

            return Ok(proposal);
        }

        // PUT: api/Proposals/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProposal(int id, Proposal proposal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != proposal.Id)
            {
                return BadRequest();
            }
            proposal.ModifiedBy = "UNKNOWN";
            proposal.Updated = DateTime.Now;
            db.Entry(proposal).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProposalExists(id))
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

        // POST: api/Proposals
        [ResponseType(typeof(Proposal))]
        public async Task<IHttpActionResult> PostProposal(Proposal proposal)
        {
            proposal.ModifiedBy = "UNKNOWN";

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Propsals.Add(proposal);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = proposal.Id }, proposal);
        }

        // DELETE: api/Proposals/5
        [ResponseType(typeof(Proposal))]
        public async Task<IHttpActionResult> DeleteProposal(int id)
        {
            Proposal proposal = await db.Propsals.FindAsync(id);
            if (proposal == null)
            {
                return NotFound();
            }

            db.Propsals.Remove(proposal);
            await db.SaveChangesAsync();

            return Ok(proposal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProposalExists(int id)
        {
            return db.Propsals.Count(e => e.Id == id) > 0;
        }
    }
}