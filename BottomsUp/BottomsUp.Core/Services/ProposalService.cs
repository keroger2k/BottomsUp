using AutoMapper;
using BottomsUp.Core.Data;
using BottomsUp.Core.Models;
using BottomsUp.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BottomsUp.Core.Services
{
    public class ProposalService : IProposalService
    {
        private readonly DatabaseContext db = new DatabaseContext();

        public ProposalService()
        {
            Mapper.CreateMap<Proposal, ProposalDTO>();
            Mapper.CreateMap<Requirement, RequirementDTO>();
            Mapper.CreateMap<Tasking, TaskDTO>();
        }

        public ProposalDTO GetProposal(int id)
        {

            var item = db.Propsals
                .Include("Requirements")
                .Include("Requirements.Tasks")
                .FirstOrDefault(c => c.Id == id);
            var propDTO = Mapper.Map<ProposalDTO>(item);
            return propDTO;
        }


        public IEnumerable<TaskDTO> GetRequirementTask(int id)
        {
            var item = db.Requirements.FirstOrDefault(c => c.Id == id);
            return item.Tasks.Select(c => Mapper.Map<TaskDTO>(c));

        }
    }
}
