using System;
using NUnit.Framework;
using System.Drawing;
using IncandescentDesigns.Controllers;
using IncandescentDesigns;

namespace TeamNUnitTest
{
    [TestFixture]
    class LaurenWeek8
    {

        public void setUp()
        {

        }
        [Test]
        public void intToHexValue()
        {
            SimulatorController sc = new SimulatorController();
            int number = 554334;
            string expectedResult = "8755E";

            string actualResult = sc.intToHex(number);
            Assert.That(actualResult, Is.EqualTo(expectedResult));

        }
        [Test]
        public void intToHexValue_small()
        {
            SimulatorController sc = new SimulatorController();
            int number = 4;
            string expectedResult = "4";

            string actualResult = sc.intToHex(number);
            Assert.That(actualResult, Is.EqualTo(expectedResult));

        }
        [Test]
        public void intToHexValue_large()
        {
            SimulatorController sc = new SimulatorController();
            int number = 554337743;
            string expectedResult = "210A85CF";

            string actualResult = sc.intToHex(number);
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
        [Test]
        public void intToHexValue_zero()
        {
            SimulatorController sc = new SimulatorController();
            int number = 0;
            string expectedResult = "0";

            string actualResult = sc.intToHex(number);
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

    }
}
