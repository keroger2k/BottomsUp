using BottomsUp.Core.Data;
using BottomsUp.Core.Models;
using BottomsUp.Web.Controllers;
using FakeItEasy;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace BottomsUp.Web.Tests
{
    [TestFixture]
    public class RequirementsControllerTests
    {

        private IBottomsRepository repo;
        private RequirementsController controller;

        [SetUp]
        public void Init()
        {
            this.repo = A.Fake<IBottomsRepository>();
            this.controller = new RequirementsController(repo);
        }

        [Test]
        public void RequirementsController_Get()
        {
            // Arrange
            A.CallTo(() => repo.GetAllProposalsWithRequirements()).Returns(GetProposals());

            // Act
            IHttpActionResult actionResult = controller.GetRequirements(1);
            var contentResult = actionResult as OkNegotiatedContentResult<ICollection<RequirementsModel>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(2, contentResult.Content.Count());
            A.CallTo(() => repo.GetAllProposalsWithRequirementsAndTasks()).MustNotHaveHappened();
            A.CallTo(() => repo.GetAllProposalsWithRequirements()).MustHaveHappened();
        }

        [Test]
        public void RequirementsController_Get_WithRequirements()
        {
            // Arrange
            A.CallTo(() => repo.GetAllProposalsWithRequirementsAndTasks()).Returns(GetProposals());

            // Act
            IHttpActionResult actionResult = controller.GetRequirements(1, includeTasks: true);
            var contentResult = actionResult as OkNegotiatedContentResult<ICollection<RequirementsModel>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(2, contentResult.Content.Count());
            A.CallTo(() => repo.GetAllProposalsWithRequirementsAndTasks()).MustHaveHappened();
            A.CallTo(() => repo.GetAllProposalsWithRequirements()).MustNotHaveHappened();
        }

        [Test]
        public void RequirementsController_Post()
        {
            // Arrange
            A.CallTo(() => repo.GetAllProposalsWithRequirements()).Returns(GetProposals());

            // Act
            IHttpActionResult actionResult = controller.PostRequirement(1, new RequirementsModel { Id = 1 }).Result;
            var contentResult = actionResult as CreatedAtRouteNegotiatedContentResult<RequirementsModel>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("requirements", contentResult.RouteName);
            Assert.AreEqual(1, contentResult.RouteValues["rid"]);
            A.CallTo(() => repo.SaveAsync()).MustHaveHappened();
        }

        [Test]
        public void RequirementsController_Post_Proposal_Not_Found()
        {
            // Arrange
            A.CallTo(() => repo.GetAllProposalsWithRequirements()).Returns(GetProposals());

            // Act
            IHttpActionResult actionResult = controller.PostRequirement(-1, new RequirementsModel { Id = 1 }).Result;
            var contentResult = actionResult as BadRequestResult;

            // Assert
            Assert.IsNotNull(contentResult);
            A.CallTo(() => repo.SaveAsync()).MustNotHaveHappened();
        }

        [Test]
        public void RequirementsController_Put_Mismatch_Ids_BadRequest()
        {
            // Arranges
            // Act
            IHttpActionResult actionResult = controller.PutRequirement(0, 1, new RequirementsModel { Id = 10 }).Result;
            var contentResult = actionResult as BadRequestResult;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsInstanceOf<BadRequestResult>(contentResult);
            A.CallTo(() => repo.UpdateProposal(A<Proposal>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => repo.SaveAsync()).MustNotHaveHappened();
        }

        [Test]
        public void RequirementsController_Put_Save_Throws_Not_Found()
        {
            // Arranges
            A.CallTo(() => repo.GetAllProposals()).Returns(GetProposals());
            A.CallTo(() => repo.SaveAsync()).Throws<DbUpdateConcurrencyException>();
            // Act
            IHttpActionResult actionResult = controller.PutRequirement(10, 1, new RequirementsModel { Id = 1 }).Result;
            var contentResult = actionResult as NotFoundResult;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsInstanceOf<NotFoundResult>(contentResult);
            A.CallTo(() => repo.SaveAsync()).MustHaveHappened();
        }

        [Test]
        public void RequirementsController_Delete()
        {
            // Arranges
            A.CallTo(() => repo.GetAllProposalsWithRequirements()).Returns(GetProposals());

            // Act
            IHttpActionResult actionResult = controller.DeleteRequirement(1, 1).Result;
            var contentResult = actionResult as OkNegotiatedContentResult<RequirementsModel>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsInstanceOf<RequirementsModel>(contentResult.Content);
            A.CallTo(() => repo.SaveAsync()).MustHaveHappened();
        }

        [Test]
        public void RequirementsController_Delete_NotFound()
        {
            // Arranges
            A.CallTo(() => repo.GetAllProposalsWithRequirements()).Returns(GetProposals());

            // Act
            IHttpActionResult actionResult = controller.DeleteRequirement(-1,-1).Result;
            var contentResult = actionResult as NotFoundResult;

            // Assert
            Assert.IsNotNull(contentResult);
            A.CallTo(() => repo.SaveAsync()).MustNotHaveHappened();
        }

        private IQueryable<Proposal> GetProposals()
        {
            return new List<Proposal>() { 
                new Proposal { 
                    Id =1, 
                    Requirements = new List<Requirement>() {
                        new Requirement { 
                            Id = 1,
                            Description = "First Requirement Description",
                            Tasks = new List<Tasking> {
                                new Tasking { Id = 1, Comments = "Comment", 
                                    Labor = new LaborCategory {
                                        Id = 1,
                                        Name = "First"
                                    }
                                },
                                new Tasking { Id = 2, Comments = "Comment", 
                                    Labor = new LaborCategory {
                                        Id = 1,
                                        Name = "First"
                                    } 
                                }
                            },
                            Category = new Category { Id = 1, Name = "Cat1" }
                        },
                        new Requirement { 
                            Id = 2,
                            Description = "Second Requirement Description",
                            Tasks = new List<Tasking> {
                                new Tasking { Id = 3, Comments = "Comment", 
                                    Labor = new LaborCategory {
                                        Id = 1,
                                        Name = "First"
                                    }
                                },
                                new Tasking { Id = 4, Comments = "Comment", 
                                    Labor = new LaborCategory {
                                        Id = 1,
                                        Name = "First"
                                    }
                                }
                            },
                            Category = new Category { Id = 1, Name = "Cat1" }
                        }
                    }
                }
            }.AsQueryable();
        }
    }
}
