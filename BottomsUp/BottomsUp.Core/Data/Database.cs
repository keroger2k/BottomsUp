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
            this.Configuration.LazyLoadingEnabled = false;
            //this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Proposal> Propsals { get; set; }
        public DbSet<Requirement> Requirements { get; set; }
        public DbSet<LaborCategory> LaborCategories { get; set; }
        public DbSet<Tasking> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Proposal>().HasMany(c => c.Requirements).WithRequired(d => d.Proposal);
            
            //modelBuilder.Entity<Requirement>()
            //    .HasMany(c => c.Tasks).WithRequired().HasForeignKey(c => c.RequirementId);

            //modelBuilder.Entity<Requirement>()
            //    .HasRequired(c => c.Category).WithMany().HasForeignKey(c => c.CategoryId);

            //modelBuilder.Entity<Proposal>()
            //   .HasMany(c => c.Requirements).WithRequired().HasForeignKey(c => c.ProposalId);

            //modelBuilder.Entity<Tasking>()
            //    .HasRequired(c => c.Labor).WithMany().HasForeignKey(e => e.LaborId);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Children).WithOptional().HasForeignKey(d => d.ParentId);
                
                
        }

    }
}
