using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System.Drawing;
using IncandescentDesigns;
using IncandescentDesigns.PatternFunctions;
using IncandescentDesigns.Controllers;
using IncandescentDesigns.Models;
using IncandescentDesigns.Handlers;
//using IncandescentDesigns.NameOfController;

namespace TeamNUnitTest
{
    [TestFixture]
    public class HowardWeek13
    {
        [Test]
        public void TestUserIsNull()
        {
            UsersController con = new UsersController();

            try
            {
                bool user = con.IsAdmin(null);
            }
            catch (ArgumentNullException e)
            {
                Assert.IsTrue(e.Message.Contains("User was null"));
            }
        }

        [Test]
        public void TestNullFakeGen()
        {

            try
            {
                FakeGenHandler gen = new FakeGenHandler(null);

            }
            catch (ArgumentNullException e)
            {
                Assert.IsTrue(e.Message.Contains("Message cannot be null"));
            }
        }

        [Test]
        public void TestEmptyFakeGen()
        {
            FakeGenHandler gen = new FakeGenHandler("");

            Assert.IsTrue(gen.ToString().Equals("#include<stdio.h>\nmain()\n{\nprintf();\n}\n"));
        }

        [Test]
        public void TestValidEmptyGen()
        {
            FakeGenHandler gen = new FakeGenHandler("test");

            Assert.IsTrue(gen.ToString().Equals("#include<stdio.h>\nmain()\n{\nprintf(test);\n}\n"));
        }
    }
}
