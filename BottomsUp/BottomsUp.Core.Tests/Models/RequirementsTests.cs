using BottomsUp.Core.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BottomsUp.Core.Tests.Models
{
    [TestFixture]
    public class RequirementsTests
    {
        [Test]
        public void requirements_total_hours_should_sum_correctly_for_single_task()
        {
            var req = new Requirement
            {
                Tasks = new List<Tasking>()
                {
                   new Tasking { Number = 1, Perecentage = 100, Volume = 1 }
                }
            };

            var result = req.TotalHours;

            Assert.AreEqual(1, result);

        }

        [Test]
        public void requirements_total_hours_should_sum_correctly_for_multiple_tasks()
        {
            var req = new Requirement
            {
                Tasks = new List<Tasking>()
                {
                   new Tasking { Number = 1, Perecentage = 100, Volume = 1 },
                   new Tasking { Number = 1, Perecentage = 50, Volume = 1 }
                }
            };

            var result = req.TotalHours;

            Assert.AreEqual(1.5M, result);

        }
    }
}
