using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BottomsUp.Core.Models
{
    public class Requirement
    {
        public Requirement()
        {
            this.Tasks = new List<Tasking>();
        }
        public int Id { get; set; }
        public string Description { get; set; }
        public string PWSNumber { get; set; }
        public string Comments { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Created { get; set; }
        public string ModifiedBy { get; set; }

        public virtual Requirement References { get; set; }
        public virtual ICollection<Tasking> Tasks { get; set; }
        public virtual Proposal Proposal { get; set; }
        public virtual Category Category { get; set; }

        public decimal TotalHours
        {
            get
            {
                return this.Tasks.Sum(c => (c.Percentage/100) * c.Volume  * c.Number);
            }
        }

    }
}
