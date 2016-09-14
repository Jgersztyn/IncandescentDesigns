using NUnit.Framework;
using IncandescentDesigns.Controllers;
using System.Web.Mvc;
using System;

namespace Sprint9Tests
{
    [TestFixture]
    public class Sprint9Tests
    {

        [Test]
        public void DropDownListTest1()
        {
            HomeController c = new HomeController();
            JsonResult result = new JsonResult();
            result = (JsonResult) c.GetPatterns("8x8");
            Assert.That(result, !Is.Null);
        }

        [Test]
        public void DropDownListTest2()
        {
            HomeController c = new HomeController();
            JsonResult result = new JsonResult();
            result = (JsonResult)c.GetPatterns("4x4x4");
            Assert.That(result, !Is.Null);
        }

        [Test]
        public void DropDownListTest3()
        {
            HomeController c = new HomeController();
            JsonResult result = new JsonResult();
            result = (JsonResult)c.GetPatterns("6x6x6");
            Assert.That(result, Is.Null);
        }
    }
}