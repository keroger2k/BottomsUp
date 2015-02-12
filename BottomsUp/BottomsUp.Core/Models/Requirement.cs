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
            this.Tasks = new List<Task>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string PWSNumber { get; set; }
        public string Comments { get; set; }

        public virtual Requirement References { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }

    }
}
