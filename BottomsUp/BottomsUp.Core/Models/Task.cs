using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BottomsUp.Core.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Perecentage { get; set; }
        public decimal Volume { get; set; }
        public string Comments { get; set; }
        public virtual LaborCategory Labor { get; set; }
    }
}
