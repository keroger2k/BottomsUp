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
    public class TaskingsController : BaseApiController
    {
        public TaskingsController(IBottomsRepository repo)
            : base(repo)
        {

        }

        // GET: api/v1/proposals/{pid}/requirements/{rid}/tasks
        [ResponseType(typeof(IEnumerable<TaskingModel>))]
        public IHttpActionResult GetTasks(int pid, int rid)
        {
            IQueryable<Proposal> props = _repo.GetAllProposalsWithRequirementsAndTasks();
            var proposal = props.FirstOrDefault(c => c.Id == pid);

            if (proposal == null)
            {
                return NotFound();
            }

            var req = proposal.Requirements.FirstOrDefault(c => c.Id == rid);

            if (req == null)
            {
                return NotFound();
            }

            var pModel = _modelFactory.Create(req);
            return Ok(pModel.Tasks);
        }

        // PUT: api/Taskings/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTasking(int pid, int rid, int tid, TaskingModel tasking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (tid != tasking.Id)
            {
                return BadRequest();
            }

            try
            {
                tasking.ModifiedBy = "UNKNOWN";
                var entity = _modelFactory.Parse(tasking);
                _repo.UpdateTasking(entity);
                await _repo.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskingExists(pid, rid, tid))
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
        [ResponseType(typeof(TaskingModel))]
        public async Task<IHttpActionResult> PostTasking(int pid, int rid, TaskingModel tasking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var proposal = _repo.GetAllProposalsWithRequirements().ToList().FirstOrDefault(c => c.Id == pid);
            if (proposal == null)
            {
                return BadRequest();
            }

            var req = proposal.Requirements.FirstOrDefault(c => c.Id == rid);
            if (req == null)
            {
                return BadRequest();
            }




            tasking.Created = DateTime.Now;
            tasking.ModifiedBy = "UNKNOWN";
            tasking.Updated = DateTime.Now;
            req.Tasks.Add(_modelFactory.Parse(tasking));

            await _repo.SaveAsync();

            return CreatedAtRoute("tasking", new { tid = tasking.Id }, tasking);
        }

        // DELETE: api/Taskings/5
        [ResponseType(typeof(TaskingModel))]
        public async Task<IHttpActionResult> DeleteTasking(int pid, int rid, int tid)
        {
            Proposal prop = _repo.GetAllProposalsWithRequirementsAndTasks()
                .FirstOrDefault(c => c.Id == pid);

            if (prop == null)
            {
                return NotFound();
            }

            Requirement requirement = prop.Requirements.FirstOrDefault(c => c.Id == rid);

            if (requirement == null)
            {
                return NotFound();
            }

            Tasking task = requirement.Tasks.FirstOrDefault(c => c.Id == tid);

            if (task == null)
            {
                return NotFound();
            }

            requirement.Tasks.Remove(task);
            await _repo.SaveAsync();

            return Ok(_modelFactory.Create(task));
        }

        private bool TaskingExists(int pid, int rid, int tid)
        {
            var proposal = _repo.GetAllProposalsWithRequirements()
                .ToList()
                .FirstOrDefault(c => c.Id == pid);
            if (proposal == null) return false;
            var req = proposal.Requirements.FirstOrDefault(c => c.Id == rid);
            return req != null &&
                req.Tasks != null &&
                req.Tasks.Count(e => e.Id == tid) > 0;
        }
    }
}