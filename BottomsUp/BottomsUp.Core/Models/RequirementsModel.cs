using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BottomsUp.Core.Models
{
    public class RequirementsModel
    {
        public int Id { get; set; }
        public int ProposalId { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string PWSNumber { get; set; }
        public string Comments { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Created { get; set; }
        public string ModifiedBy { get; set; }

        public Requirement References { get; set; }
        public ICollection<TaskingModel> Tasks { get; set; }

        public Category Category { get; set; }

        public decimal TotalHours
        {
            get
            {
                return this.Tasks.Sum(c => (c.Percentage/100) * c.Volume  * c.Number);
            }
        }
    }
}
