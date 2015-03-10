using BottomsUp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BottomsUp.Core.Data
{
    public interface IBottomsRepository
    {
        void AddProposal(Proposal proposal);
        IQueryable<Proposal> GetAllProposals();
        IQueryable<Proposal> GetAllProposalsWithRequirements();
        Task<Proposal> GetProposalAsync(int id);
        Task<Proposal> GetProposalWithRequirementsAsync(int id);
        void RemoveProposal(int id);
        Task<int> SaveAsync();
        void UpdateProposal(Proposal proposal);
    }
}
