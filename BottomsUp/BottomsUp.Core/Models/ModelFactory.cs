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
                Category = new Category
                {
                    Id = requirement.Category.Id,
                    Name = requirement.Category.Name
                },
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
                Labor = new LaborCategory {
                    Id = task.Labor.Id,
                    Name = task.Labor.Name
                },
                ModifiedBy = task.ModifiedBy,
                Number = task.Number,
                Percentage = task.Percentage,
                Updated = task.Updated,
                Volume = task.Volume
            };
        }

        public Proposal Parse(ProposalModel proposal)
        {
            try
            {
                var entry = new Proposal();
                entry.Id = proposal.Id;
                entry.Name = proposal.Name;
                entry.ModifiedBy = proposal.ModifiedBy;
                entry.Updated = proposal.Updated;
                entry.Created = proposal.Created;
                return entry;
            }
            catch
            {
                throw;
            }
        }

        public Requirement Parse(RequirementsModel requirement)
        {
            try
            {
                var entry = new Requirement();
                entry.PWSNumber = requirement.PWSNumber;
                entry.Id = requirement.Id;
                entry.Description = requirement.Description;
                entry.Created = requirement.Created;
                entry.Updated = requirement.Updated;
                entry.References = requirement.References;
                return entry;
            }
            catch
            {
                throw;
            }
        }

        public Tasking Parse(TaskingModel task)
        {
            try
            {
                var entry = new Tasking();
                entry.Comments = task.Comments;
                entry.Created = task.Created;
                entry.Description = task.Description;
                entry.Id = task.Id;
                entry.Labor = task.Labor;
                entry.ModifiedBy = task.ModifiedBy;
                entry.Number = task.Number;
                entry.Percentage = task.Percentage;
                entry.Updated = task.Updated;
                entry.Volume = task.Volume;
                return entry;
            }
            catch
            {
                throw;
            }
        }
    }
}
