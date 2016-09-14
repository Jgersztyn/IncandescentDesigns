using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System.Drawing;
using IncandescentDesigns.Handlers;
using IncandescentDesigns;
//using IncandescentDesigns.NameOfController;

namespace TeamNUnitTest
{
    [TestFixture]
    public class HowardWeek8
    {
        [Test]
        public void TestFormatNameNull()
        {
            string tmp = ImageHandler.FormatName(null);
            Assert.IsNull(tmp);
        }

        [Test]
        public void TestFormatNoDelims()
        {
            string tmp = ImageHandler.FormatName("fafs");
            Assert.IsNotNull(tmp);
            Assert.True(tmp.Equals("fafs"));
        }

        [Test]
        public void TestFormatForwardSlash()
        {
            string tmp = ImageHandler.FormatName("f/a/f/s");
            Assert.IsNotNull(tmp);
            Assert.True(tmp.Equals("s"));
        }

        [Test]
        public void TestFormatBackSlash()
        {
            string tmp = ImageHandler.FormatName("f\\a\\f\\s");
            Assert.IsNotNull(tmp);
            Assert.True(tmp.Equals("s"));
        }

        [Test]
        public void TestFormatBothDelims()
        {
            string tmp = ImageHandler.FormatName("T/hi\\sIs/ALo\\ngS/tr\\ing");
            Assert.IsNotNull(tmp);
            Assert.True(tmp.Equals("ing"));
        }

    }
}
