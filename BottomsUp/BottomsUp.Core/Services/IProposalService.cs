using BottomsUp.Core.Models.DTOs;
using System;
using System.Collections.Generic;
namespace BottomsUp.Core.Services
{
    public interface IProposalService
    {
        ProposalDTO GetProposal(int id);
        IEnumerable<TaskDTO> GetRequirementTask(int id);
    }
}
