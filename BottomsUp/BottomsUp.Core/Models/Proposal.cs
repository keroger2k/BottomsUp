﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BottomsUp.Core.Models
{
    public class Proposal
    {
        public Proposal()
        {
            this.Requirements = new List<Requirement>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public DateTime Updated { get; set; }
        public DateTime Created { get; set; }
        public string ModifiedBy { get; set; }

        public virtual ICollection<Requirement> Requirements { get; set; }
    }
}
