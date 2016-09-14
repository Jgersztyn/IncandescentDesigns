using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System.Drawing;
using IncandescentDesigns.Handlers;
using IncandescentDesigns;
using IncandescentDesigns.PatternFunctions;
//using IncandescentDesigns.NameOfController;

namespace TeamNUnitTest
{
    [TestFixture]
    public class HowardWeek10
    {
        [Test]
        public void TestJSPatternsNoParams()
        {
            JSPatterns pattern = new JSPatterns();
            bool empty = false;
            if (pattern.theColor == null && pattern.theCubeSize == null && pattern.theDuration == null && pattern.theFileName == null && pattern.thePattern == null && pattern.theSpeed == null)
            {
                empty = true;
            }

            Assert.True(empty);
        }

        [Test]
        public void TestJSPatternsEmptyParam()
        {
            JSPatterns pattern = new JSPatterns();
            pattern.theSpeed = pattern.thePattern = pattern.theFileName = pattern.theDuration = pattern.theCubeSize = pattern.theColor = "";
            bool empty = false;
            if (pattern.theColor.Equals("") && pattern.theCubeSize.Equals("") && pattern.theDuration.Equals("") && pattern.theFileName.Equals("") && pattern.thePattern.Equals("") && pattern.theSpeed.Equals(""))
            {
                empty = true;
            }

            Assert.True(empty);
        }

        [Test]
        public void TestJSPatternFullParam()
        {
            JSPatterns pattern = new JSPatterns();
            pattern.theSpeed = pattern.thePattern = pattern.theFileName = pattern.theDuration = pattern.theCubeSize = pattern.theColor = "Test";
            bool empty = false;
            if (pattern.theColor.Equals("Test") && pattern.theCubeSize.Equals("Test") && pattern.theDuration.Equals("Test") && pattern.theFileName.Equals("Test") && pattern.thePattern.Equals("Test") && pattern.theSpeed.Equals("Test"))
            {
                empty = true;
            }

            Assert.True(empty);
        }

        [Test]
        public void TestJSPatternInitMatrix()
        {
            Assert.IsNotNull(JSPatterns.initializeMatrix());
            Assert.True(JSPatterns.initializeMatrix().Equals(""));
        }

        [Test]
        public void TestJSPatternStartLoop()
        {
            Assert.IsNotNull(JSPatterns.startLoop());
            Assert.True(JSPatterns.startLoop().Equals(""));
        }

        [Test]
        public void TestJSPatternEndLoop()
        {
            Assert.IsNotNull(JSPatterns.endLoop());
            Assert.True(JSPatterns.endLoop().Equals(""));
        }

        [Test]
        public void TestJSPatternGetDefaultSpeed()
        {
            Assert.IsNotNull(JSPatterns.getDefaultSpeed());
            Assert.True(JSPatterns.getDefaultSpeed().Equals("500"));
        }

        [Test]
        public void TestJSPatternGetDefaultColor()
        {
            Assert.IsNotNull(JSPatterns.getDefaultColor());
            Assert.True(JSPatterns.getDefaultColor().Equals("FF0000"));
        }

        [Test]
        public void TestJSPatternGetDefaultDuration()
        {
            Assert.IsNotNull(JSPatterns.getDefaultDuration());
            Assert.True(JSPatterns.getDefaultDuration().Equals("5"));
        }

        [Test]
        public void TestJSPatternGetDefaultPattern()
        {
            Assert.IsNotNull(JSPatterns.getDefaultPattern());
            Assert.True(JSPatterns.getDefaultPattern().Equals("twoDimLetters"));
        }
    }
}
