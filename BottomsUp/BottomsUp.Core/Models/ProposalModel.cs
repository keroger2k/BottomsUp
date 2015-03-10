using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BottomsUp.Core.Models
{
    public class ProposalModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public DateTime Updated { get; set; }
        public DateTime Created { get; set; }
        public string ModifiedBy { get; set; }

        public ICollection<RequirementsModel> Requirements { get; set; }
    }
}
