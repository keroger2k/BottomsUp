using BottomsUp.Core.Data;
using BottomsUp.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace BottomsUp.Web.Controllers
{
    public class ProposalsController : BaseController
    {
        public ProposalsController(IBottomsRepository repo)
            : base(repo)
        {

        }

        // GET: api/v1/proposals
        [ResponseType(typeof(ProposalModel))]
        public IHttpActionResult GetPropsals(bool includeRequirements = false)
        {
            IEnumerable<Proposal> query;
            if (includeRequirements)
            {
                query = _repo.GetAllProposalsWithRequirements();
            }
            else
            {
                query = _repo.GetAllProposals();
            }
            return Ok(query.ToList().Select(c => _modelFactory.Create(c)));
        }

        // GET: api/v1/proposals/5
        [ResponseType(typeof(ProposalModel))]
        public async Task<IHttpActionResult> GetProposal(int pid, bool includeRequirements = false)
        {
            Proposal prop;

            if (includeRequirements)
            {
                prop = await _repo.GetProposalWithRequirementsAsync(pid);
            }
            else
            {
                prop = await _repo.GetProposalAsync(pid);
            }

            if (prop == null)
            {
                return NotFound();
            }

            return Ok(_modelFactory.Create(prop));
        }

        // PUT: api/v1/proposals/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProposal(int id, ProposalModel proposal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != proposal.Id)
            {
                return BadRequest();
            }

            try
            {
                proposal.ModifiedBy = "UNKNOWN";
                var entity = _modelFactory.Parse(proposal);
                _repo.UpdateProposal(entity);
                await _repo.SaveAsync();
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

        // POST: api/v1/proposals
        [ResponseType(typeof(ProposalModel))]
        public async Task<IHttpActionResult> PostProposal(ProposalModel proposal)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                proposal.ModifiedBy = "UNKNOWN";
                var entity = _modelFactory.Parse(proposal);
                _repo.AddProposal(entity);
                await _repo.SaveAsync();
                return CreatedAtRoute("proposals", new { id = entity.Id }, entity);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        // DELETE: api/v1/proposals/5
        [ResponseType(typeof(ProposalModel))]
        public async Task<IHttpActionResult> DeleteProposal(int id)
        {
            Proposal proposal = await _repo.GetProposalAsync(id);
            if (proposal == null)
            {
                return NotFound();
            }

            _repo.RemoveProposal(proposal.Id);

            await _repo.SaveAsync();

            return Ok(proposal);
        }

        private bool ProposalExists(int id)
        {
            return _repo.GetAllProposals().Count(e => e.Id == id) > 0;
        }
    }
}