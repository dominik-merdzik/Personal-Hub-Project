using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalHub.Controllers;
using PersonalHub.Data;

namespace PersonalHubTests
{
    [TestClass]
    public class SchedulesControllerTest
    {
        [TestMethod]
        public void IndexLoadsView()
        {

            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new ApplicationDbContext(options);
            var controller = new SchedulesController(context);

            // Act
            var result = (ViewResult)controller.Index().Result;

            // Assert
            Assert.AreEqual("Index", result.ViewName);


        }
    }
}