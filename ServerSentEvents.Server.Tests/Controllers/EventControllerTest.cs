using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerSentEvents.Server;
using ServerSentEvents.Server.Controllers;

namespace ServerSentEvents.Server.Tests.Controllers
{
    [TestClass]
    public class EventControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            EventStreamController controller = new EventStreamController();

            // Act
            //ViewResult result = controller.Index() as ViewResult;

            // Assert
            //Assert.IsNotNull(result);
            //Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
