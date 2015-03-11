using BottomsUp.Core.Data;
using BottomsUp.Core.Models;
using BottomsUp.Web.Controllers;
using FakeItEasy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public void get_returns_proposal_list()
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
        }

        //[Test]
        //public async void GetProposal_ShouldNotFindProduct()
        //{
        //    //// Arrange
        //    //var repo = A.Fake<IBottomsRepository>();

        //    //A.CallTo(() => repo.GetProposalAsync(999)).Returns(null);

        //    //var controller = new ProposalsController(repo);

        //    //IHttpActionResult result = await controller.GetProposal(999) as OkNegotiatedContentResult<ProposalModel>;
        //    //Assert.IsInstanceOfType(typeof(NotFoundResult), result);
        //}

        private IQueryable<Proposal> GetProposals()
        {
            return new List<Proposal>() { 
                new Proposal { Id =1, Requirements = 
                    new List<Requirement>() {
                        new Requirement { Id = 1 }
                    }
                }
            }.AsQueryable();
        }
    }
}
