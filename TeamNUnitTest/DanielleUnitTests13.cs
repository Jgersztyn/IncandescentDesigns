using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System.Drawing;
using IncandescentDesigns.Handlers;
using IncandescentDesigns;
using IncandescentDesigns.PatternFunctions;
using IncandescentDesigns.Controllers;
//using IncandescentDesigns.NameOfController;

namespace TeamNUnitTest
{
    class DanielleUnitTests13
    {
        [Test]
        public void TestCheckSpeedNotFloat()
        {
            string input = "0.0005ggggg";
            bool result = !Patterns.checkSpeed(input);

            Assert.That(result);
        }

        [Test]
        public void TestCheckSpeedIsFloat()
        {
            string input = "0.0005";
            bool result = Patterns.checkSpeed(input);

            Assert.That(result);
        }

        [Test]
        public void TestCheckDurationNotInt()
        {
            string input = "we12der";
            bool result = !Patterns.checkDuration(input);

            Assert.That(result);
        }

        [Test]
        public void TestCheckDurationIsInt()
        {
            string input = "12";
            bool result = Patterns.checkDuration(input);

            Assert.That(result);
        }
    }
}
