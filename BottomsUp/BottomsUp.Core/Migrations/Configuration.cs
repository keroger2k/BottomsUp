namespace BottomsUp.Core.Migrations
{
    using BottomsUp.Core.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BottomsUp.Core.Data.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BottomsUp.Core.Data.DatabaseContext context)
        {
            context.LaborCategories.AddOrUpdate(l => l.Name,
                new LaborCategory { Name = "Software Engineer I" },
                new LaborCategory { Name = "Software Engineer II" },
                new LaborCategory { Name = "Software Engineer III" },
                new LaborCategory { Name = "Software Engineer IV" },
                new LaborCategory { Name = "Software Engineer V" }
            );

            context.Categories.AddOrUpdate(c => c.Name,
                new Category { Name = "Category1" },
                new Category { Name = "Category2" },
                new Category { Name = "Category1Sub1", ParentId = 1 },
                new Category { Name = "Category1Sub2", ParentId = 1 },
                new Category { Name = "Category2Sub1", ParentId = 2 }
            );

            context.SaveChanges();

            context.Propsals.AddOrUpdate(c => c.Name,
                new Proposal
                {
                    Name = "Proposal #1",
                    Created = DateTime.Now,
                    ModifiedBy = "Kyle Rogers",
                    Updated = DateTime.Now,
                    Requirements = new List<Requirement>()
                    {
                        new Requirement { 
                            PWSNumber = "1.1", 
                            Description = "First Requirement Description", 
                            Comments = "First Requirements Comments #1", 
                            Created = DateTime.Now, 
                            ModifiedBy = "Kyle Rogers", 
                            Updated = DateTime.Now,
                            CategoryId = 1,
                            Tasks = new List<Tasking> {
                                new Tasking { 
                                    Number = 1, 
                                    Percentage = 100, 
                                    Volume = 1, 
                                    Updated = DateTime.Now, 
                                    Comments = "First Requirement First Task Comment #1",
                                    Created = DateTime.Now, 
                                    Description = "First Requirement First Task Description #1",
                                    LaborId = 1
                                }
                            }
                        },
                        new Requirement { 
                            PWSNumber = "1.1", 
                            Description = "Second Requirement Description", 
                            Comments = "Second Requirements Comments #1", 
                            Created = DateTime.Now, 
                            ModifiedBy = "Kyle Rogers", 
                            Updated = DateTime.Now, 
                            CategoryId = 3,
                            Tasks = new List<Tasking> {
                                new Tasking { 
                                    Number = 1, 
                                    Percentage = 100, 
                                    Volume = 1, 
                                    Updated = DateTime.Now, 
                                    Comments = "Second Requirement First Task Comment #1",
                                    Created = DateTime.Now, 
                                    Description = "Second Requirement First Task Description #1",
                                    LaborId = 1
                                }
                            }
                        }
                    }
                });

            context.SaveChanges();
        }
    }
}
