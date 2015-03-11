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
    public class RequirementsController : BaseController
    {
        public RequirementsController(IBottomsRepository repo)
            : base(repo)
        {

        }
       
        // GET: api/Requirements/5
        [ResponseType(typeof(RequirementsModel))]
        public async Task<IHttpActionResult> GetRequirements(int pid, bool includeTasks = false)
        {
            IQueryable<Proposal> props;

            if (includeTasks)
            {
                props = _repo.GetAllProposalsWithRequirementsAndTasks();
            }
            else
            {
                props = _repo.GetAllProposalsWithRequirements();
            }

            var proposal = await props.FirstOrDefaultAsync(c => c.Id == pid);


            if (proposal == null)
            {
                return NotFound();
            }
            var pModel = _modelFactory.Create(proposal);
            return Ok(pModel.Requirements);
        }

        // PUT: api/Requirements/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRequirement(int pid, int rid, RequirementsModel requirement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (rid != requirement.Id)
            {
                return BadRequest();
            }

            try
            {
                requirement.ModifiedBy = "UNKNOWN";
                var entity = _modelFactory.Parse(requirement);
                _repo.UpdateRequirement(entity);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequirementExists(pid, requirement.Id))
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

        //// POST: api/Requirements
        //[ResponseType(typeof(Requirement))]
        //public async Task<IHttpActionResult> PostRequirement(int pid, Requirement requirement)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var proposal = db.Propsals.Find(pid);
        //    if (proposal == null)
        //    {
        //        return BadRequest();
        //    }

        //    requirement.Created = DateTime.Now;
        //    requirement.ModifiedBy = "UNKNOWN";
        //    requirement.Updated = DateTime.Now;
        //    proposal.Requirements.Add(requirement);

        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("requirements", new { rid = requirement.Id }, requirement);
        //}

        //// DELETE: api/Requirements/5
        //[ResponseType(typeof(Requirement))]
        //public async Task<IHttpActionResult> DeleteRequirement(int id)
        //{
        //    Requirement requirement = await db.Requirements.FindAsync(id);
        //    if (requirement == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Requirements.Remove(requirement);
        //    await db.SaveChangesAsync();

        //    return Ok(requirement);
        //}

        private bool RequirementExists(int pid, int rid)
        {
            var proposal = _repo.GetAllProposalsWithRequirements().FirstOrDefault(c => c.Id == pid);
            return proposal != null &&
                proposal.Requirements != null &&
                proposal.Requirements.Count(e => e.Id == rid) > 0;
        }
    }
}