using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BottomsUp.Core.Models
{
    public class ModelFactory
    {
        public ProposalModel Create(Proposal proposal)
        {
            return new ProposalModel
            {
                Id = proposal.Id,
                Name = proposal.Name,
                Updated = proposal.Updated,
                Created = proposal.Created,
                ModifiedBy = proposal.ModifiedBy,
                Requirements = proposal.Requirements.Select(c => Create(c)).ToList()
            };
        }


        public RequirementsModel Create(Requirement requirement)
        {
            return new RequirementsModel
            {
                Category = requirement.Category,
                Comments = requirement.Comments,
                Updated = requirement.Updated,
                Created = requirement.Created,
                Description = requirement.Description,
                Id = requirement.Id,
                ModifiedBy = requirement.ModifiedBy,
                PWSNumber = requirement.PWSNumber,
                References = requirement.References,
                Tasks = requirement.Tasks.Select(c => Create(c)).ToList()
            };
        }

        public TaskingModel Create(Tasking task)
        {
            return new TaskingModel
            {
                Comments = task.Comments,
                Created = task.Created,
                Description = task.Description,
                Id = task.Id,
                Labor = task.Labor,
                ModifiedBy = task.ModifiedBy,
                Number = task.Number,
                Percentage = task.Percentage,
                Updated = task.Updated,
                Volume = task.Volume
            };
        }

    }
}
