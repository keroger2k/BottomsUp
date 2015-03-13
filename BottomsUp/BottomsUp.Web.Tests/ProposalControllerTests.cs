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
    public class ProposalControllerTests
    {

        private IBottomsRepository repo;
        private ProposalsController controller;

        [SetUp]
        public void Init()
        {
            this.repo = A.Fake<IBottomsRepository>();
            this.controller = new ProposalsController(repo);
        }

        [Test]
        public void ProposalsController_Get()
        {
            // Arrange
            A.CallTo(() => repo.GetAllProposals()).Returns(GetProposals());


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
        public void ProposalsController_Get_WithRequirements()
        {
            // Arrange
            A.CallTo(() => repo.GetAllProposalsWithRequirements()).Returns(GetProposals());

            // Act
            IHttpActionResult actionResult = controller.GetPropsals(includeRequirements: true);
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<ProposalModel>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.Count());
            A.CallTo(() => repo.GetAllProposalsWithRequirements()).MustHaveHappened();
            A.CallTo(() => repo.GetAllProposals()).MustNotHaveHappened();
        }

        [Test]
        public void ProposalController_Post()
        {
            // Arranges
            // Act
            IHttpActionResult actionResult = controller.PostProposal(new ProposalModel { Id= 10 }).Result;
            var contentResult = actionResult as CreatedAtRouteNegotiatedContentResult<Proposal>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("proposals", contentResult.RouteName);
            Assert.AreEqual(10, contentResult.RouteValues["id"]);
            A.CallTo(() => repo.AddProposal(A<Proposal>.Ignored)).MustHaveHappened();
            A.CallTo(() => repo.SaveAsync()).MustHaveHappened();
        }

        [Test]
        public void ProposalController_Invalid_Post()
        {
            // Arranges
            // Act
            IHttpActionResult actionResult = controller.PostProposal(null).Result;
            var contentResult = actionResult as BadRequestResult;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsInstanceOf<BadRequestResult>(contentResult);
            A.CallTo(() => repo.AddProposal(A<Proposal>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => repo.SaveAsync()).MustNotHaveHappened();
        }

        [Test]
        public void ProposalController_Put_Mismatch_Ids_BadRequest()
        {
            // Arranges
            // Act
            IHttpActionResult actionResult = controller.PutProposal(0, new ProposalModel { Id = 10 }).Result;
            var contentResult = actionResult as BadRequestResult;

            // Assert
            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsInstanceOf<BadRequestResult>(contentResult);
            A.CallTo(() => repo.UpdateProposal(A<Proposal>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => repo.SaveAsync()).MustNotHaveHappened();
        }

        [Test]
        public void ProposalController_Put_Save_Throws_Not_Found()
        {
            // Arranges
            A.CallTo(() => repo.GetAllProposals()).Returns(GetProposals());
            A.CallTo(() => repo.SaveAsync()).Throws<DbUpdateConcurrencyException>();
            // Act
            IHttpActionResult actionResult = controller.PutProposal(10, new ProposalModel { Id = 10 }).Result;
            var contentResult = actionResult as NotFoundResult;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsInstanceOf<NotFoundResult>(contentResult);
            A.CallTo(() => repo.UpdateProposal(A<Proposal>.Ignored)).MustHaveHappened();
            A.CallTo(() => repo.SaveAsync()).MustHaveHappened();
        }

        [Test]
        public void ProposalController_Delete()
        {
            // Arranges
            A.CallTo(() => repo.GetProposalAsync(A<int>.Ignored)).Returns(GetProposals().First());

            // Act
            IHttpActionResult actionResult = controller.DeleteProposal(1).Result;
            var contentResult = actionResult as OkNegotiatedContentResult<ProposalModel>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsInstanceOf<ProposalModel>(contentResult.Content);
            A.CallTo(() => repo.RemoveProposal(A<int>.Ignored)).MustHaveHappened();
            A.CallTo(() => repo.SaveAsync()).MustHaveHappened();
        }

        [Test]
        public void ProposalController_Delete_NotFound()
        {
            // Arranges
            A.CallTo(() => repo.GetProposalAsync(A<int>.Ignored))
                .Returns(Task.FromResult(default(Proposal)));
             
            // Act
            IHttpActionResult actionResult = controller.DeleteProposal(1).Result;
            var contentResult = actionResult as NotFoundResult;

            // Assert
            Assert.IsNotNull(contentResult);
            A.CallTo(() => repo.RemoveProposal(A<int>.Ignored)).MustNotHaveHappened();
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
                            Category = new Category { Id = 1, Name = "Cat1" }
                        }
                    }
                }
            }.AsQueryable();
        }
    }
}
