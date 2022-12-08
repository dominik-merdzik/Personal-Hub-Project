using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonalHub.Controllers;
using PersonalHub.Data;
using PersonalHub.Models;

namespace PersonalHubTests
{
    [TestClass]
    public class SchedulesControllerTest
    {

        // db var at class level for use in all tests
        private ApplicationDbContext context;
        SchedulesController controller;

        // set up code that runs automatically before each unit test
        [TestInitialize]
        public void TestInitialize()
        {
            // must instantiate in memory db to pass as a dependency when creating an instance of ProductsController
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            context = new ApplicationDbContext(options);

            // seed the db before passing it to controller
            var category = new Category { CategoryId = 212, Name = "Some Category " };
            context.Add(category);

            for (var i = 100; i < 111; i++)
            {
                var product = new Schedule { ScheduleId = i, Title = "Schedule " + i.ToString(), CategoryId = 111, Category = category, Date = "07/12/2022"};
                context.Add(product);
            }

            var extraProduct = new Schedule { ScheduleId = 300, Title = "TWO Schedule", CategoryId = 222, Category = category, Date = "07/13/2022" };
            context.Add(extraProduct);
            context.SaveChanges();

            controller = new SchedulesController(context);
        }

        #region "Index Tests"
        [TestMethod]
        public void IndexLoadsView()
        {

            // Arrange
            // Done in TestInitialize

            // Act
            var result = (ViewResult)controller.Index().Result;

            // Assert
            Assert.AreEqual("Index", result.ViewName);

        }
        [TestMethod]
        public void IndexLoadsProducts()
        {
            // act
            var result = (ViewResult)controller.Index().Result;
            List<Schedule> model = (List<Schedule>)result.Model;

            // assert
            CollectionAssert.AreEqual(context.Schedules.OrderBy(p => p.Title).ToList(), model);
        }
        #endregion

        #region "Details Tests"
        [TestMethod]
        public void DetailsLoadsViewNoId404()
        {
            // act
            var result = (ViewResult)controller.Details(null).Result;

            // assert
            Assert.AreEqual("404", result.ViewName);
        }
        [TestMethod]
        public void DetailsLoadsProduct()
        {
            // act
            var result = (ViewResult)controller.Details(100).Result;
            Schedule model = (Schedule)result.Model;

            // assert
            Assert.AreEqual(context.Schedules.Find(100), model);
        }
        [TestMethod]
        public void DetailsNoProductsTableLoads404()
        {
            // arrange
            context.Schedules = null;

            // act
            var result = (ViewResult)controller.Details(null).Result;

            // assert 
            Assert.AreEqual("404", result.ViewName);
        }

        #endregion






        
        #region "Create Tests"
        [TestMethod]
        public void CreateLoadsView()
        {
            // act
            var result = (ViewResult)controller.Create();

            // assert
            Assert.AreEqual("Create", result.ViewName);
        }
        [TestMethod]
        public void CreateLoadsCategories()
        {
            // act
            var result = (ViewResult)controller.Create();
            SelectList model = (SelectList)result.ViewData["CategoryId"];

            // assert
            CollectionAssert.AreEqual(context.Category.OrderBy(c => c.Name).ToList(), (System.Collections.ICollection)model);
        }
        [TestMethod]
        public void CreateLoadsCategoriesNoTable()
        {
            // arrange
            context.Category = null;

            // act
            var result = (ViewResult)controller.Create();
            SelectList model = (SelectList)result.ViewData["CategoryId"];

            // assert
            Assert.AreEqual(0, model.Count());
        }
        [TestMethod]
        public void CreateLoadsCategoriesEmptyTable()
        {
            // arrange
            context.Category.RemoveRange(context.Category);
            context.SaveChanges();

            // act
            var result = (ViewResult)controller.Create();
            SelectList model = (SelectList)result.ViewData["CategoryId"];

            // assert
            Assert.AreEqual(0, model.Count());
        }
        [TestMethod]
        public void CreateLoadsCategoriesNoProducts()
        {
            // arrange
            context.Schedules.RemoveRange(context.Schedules);
            context.SaveChanges();

            // act
            var result = (ViewResult)controller.Create();
            SelectList model = (SelectList)result.ViewData["CategoryId"];

            // assert
            CollectionAssert.AreEqual(context.Category.OrderBy(c => c.Name).ToList(), (System.Collections.ICollection)model);
        }
        #endregion 



        


        
        #region "Edit Tests"
        [TestMethod]
        public void EditLoadsViewNoId404()
        {
            // act
            var result = (ViewResult)controller.Edit(null).Result;

            // assert
            Assert.AreEqual("404", result.ViewName);
        }
        [TestMethod]
        public void EditLoadsProduct()
        {
            // act
            var result = (ViewResult)controller.Edit(100).Result;
            Schedule model = (Schedule)result.Model;

            // assert
            Assert.AreEqual(context.Schedules.Find(100), model);
        }


        #endregion




        
        #region "Delete Tests"
        [TestMethod]
        public void DeleteLoadsViewNoId404()
        {
            // act
            var result = (ViewResult)controller.Delete(null).Result;

            // assert
            Assert.AreEqual("404", result.ViewName);
        }
        [TestMethod]
        public void DeleteLoadsProduct()
        {
            // act
            var result = (ViewResult)controller.Delete(100).Result;
            Schedule model = (Schedule)result.Model;

            // assert
            Assert.AreEqual(context.Schedules.Find(100), model);
        }
        [TestMethod]
        public void DeleteNoProductsTableLoads404()
        {
            // arrange
            context.Schedules = null;

            // act
            var result = (ViewResult)controller.Delete(null).Result;

            // assert 
            Assert.AreEqual("404", result.ViewName);
        }
        #endregion

    }
}
