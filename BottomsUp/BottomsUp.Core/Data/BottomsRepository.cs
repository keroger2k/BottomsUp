using BottomsUp.Core.Data;
using BottomsUp.Core.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BottomsUp.Core.Data
{
    public class BottomsRepository : IBottomsRepository
    {
        private readonly DatabaseContext _db;
        public BottomsRepository(DatabaseContext db)
        {
            this._db = db;
        }

        public IQueryable<Proposal> GetAllProposals()
        {
            return _db.Propsals;
        }

        public IQueryable<Proposal> GetAllProposalsWithRequirements()
        {
            return _db.Propsals
                .Include("Requirements")
                .Include("Requirements.Category");
        }

        public IQueryable<Proposal> GetAllProposalsWithRequirementsAndTasks()
        {
            return _db.Propsals
                .Include("Requirements")
                .Include("Requirements.Category")
                .Include("Requirements.Tasks")
                .Include("Requirements.Tasks.Labor");
        }

        public async Task<Proposal> GetProposalAsync(int id)
        {
            return await _db.Propsals.FindAsync(id);
        }

        public async Task<Proposal> GetProposalWithRequirementsAsync(int id)
        {
            return await _db.Propsals
                .Include("Requirements")
                .Include("Requirements.Category")
                .Include("Requirements.Tasks")
                .Include("Requirements.Tasks.Labor")
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public void AddProposal(Proposal proposal)
        {
            proposal.Created = DateTime.Now;
            proposal.Updated = DateTime.Now;
            _db.Propsals.Add(proposal);
        }

        public void UpdateProposal(Proposal proposal)
        {
            proposal.Updated = DateTime.Now;
            _db.Entry(proposal).State = EntityState.Modified;
        }

        public void UpdateRequirement(Requirement requirement)
        {
            requirement.Updated = DateTime.Now;
            _db.Entry(requirement).State = EntityState.Modified;
        }

        public void UpdateTasking(Tasking task)
        {
            task.Updated = DateTime.Now;
            _db.Entry(task).State = EntityState.Modified;
        }

        public void RemoveProposal(int id)
        {
            Proposal proposal = _db.Propsals.Find(id);
            if (proposal != null)
            {
                _db.Propsals.Remove(proposal);
            }
        }

        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
