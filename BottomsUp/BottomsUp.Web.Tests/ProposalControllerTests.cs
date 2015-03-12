using BottomsUp.Core.Data;
using BottomsUp.Core.Models;
using BottomsUp.Web.Controllers;
using FakeItEasy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace BottomsUp.Web.Tests
{
    [TestFixture]
    public class ProposalControllerTests
    {
        [Test]
        public void ProposalsController_Get()
        {
            // Arrange
            var repo = A.Fake<IBottomsRepository>();

            A.CallTo(() => repo.GetAllProposals()).Returns(GetProposals());

            var controller = new ProposalsController(repo);

            // Act
            IHttpActionResult actionResult = controller.GetPropsals();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<ProposalModel>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.Count());
            A.CallTo(() => repo.GetAllProposals()).MustHaveHappened();
            A.CallTo(() => repo.GetAllProposalsWithRequirements()).MustNotHaveHappened();
        }

        [Test]
        public void ProposalController_Post()
        {
            // Arranges
            var repo = A.Fake<IBottomsRepository>();
            var controller = new ProposalsController(repo);

            // Act
            IHttpActionResult actionResult = controller.PostProposal(new ProposalModel { Id= 10 }).Result;
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Proposal>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("proposals", createdResult.RouteName);
            Assert.AreEqual(10, createdResult.RouteValues["id"]);

        }

        private IQueryable<Proposal> GetProposals()
        {
            return new List<Proposal>() { 
                new Proposal { 
                    Id =1, 
                    Requirements = new List<Requirement>() {
                        new Requirement { 
                            Id = 1,
                            Category = new Category { Id = 1, Name = "Cat1" }
                        }
                    }
                }
            }.AsQueryable();
        }
    }
}
