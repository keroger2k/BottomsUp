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
            return _db.Propsals.Include(c => c.Requirements);
        }

        public async Task<Proposal> GetProposalAsync(int id)
        {
            return await _db.Propsals.FindAsync(id);
        }

        public async Task<Proposal> GetProposalWithRequirementsAsync(int id)
        {
            return await _db.Propsals.Include("Requirements").FirstOrDefaultAsync(c => c.Id == id);
        }

        public void AddProposal(Proposal proposal)
        {
            proposal.Created = DateTime.Now;
            proposal.Updated = DateTime.Now;
            _db.Propsals.Add(proposal);
        }

        public void UpdateProposal(Proposal proposal)
        {
            var p = GetProposalAsync(proposal.Id);
            if (p != null)
            {
                proposal.Updated = DateTime.Now;
                _db.Entry(proposal).State = EntityState.Modified;   
            }
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
