using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BottomsUp.Core.Models.DTOs
{
    public class RequirementDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string PWSNumber { get; set; }
        public string Comments { get; set; }
        public bool Deleted { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Created { get; set; }
        public string ModifiedBy { get; set; }
        public decimal TotalHours { get; set; }
    }
}
