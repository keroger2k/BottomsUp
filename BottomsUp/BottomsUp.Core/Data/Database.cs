using BottomsUp.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BottomsUp.Core.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<Proposal> Propsals { get; set; }
        public DbSet<Requirement> Requirements { get; set; }

    }
}
