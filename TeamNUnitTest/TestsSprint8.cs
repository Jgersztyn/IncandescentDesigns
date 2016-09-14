using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System.Drawing;
using IncandescentDesigns.Handlers;
using IncandescentDesigns;
using IncandescentDesigns.PatternFunctions;
using System.Collections.Generic;
using System.Linq;
//using IncandescentDesigns.NameOfController;

namespace TeamNUnitTest
{
    class TestsSprint8
    {
        [Test]
        public void testInitializeMatrixNotNull()
        {
            Assert.IsNotNull(Patterns.initializeMatrix());
        }

        [Test]
        public void testInitializeMatrixNotEmpty()
        {
            Assert.IsNotEmpty(Patterns.initializeMatrix());
        }

        [Test]
        public void testInitializeMatrix()
        {
            string expected = "#include <Rainbowduino.h>\n#include <stdlib.h>\n\nvoid setup()\n{\n\tRb.init();\n}\n";
            Assert.True(expected.Equals(Patterns.initializeMatrix()));
        }

        [Test]
        public void startLoopNotNull()
        {
            Assert.IsNotNull(Patterns.startLoop());
        }

        [Test]
        public void startLoopNotEmpty()
        {
            Assert.IsNotEmpty(Patterns.startLoop());
        }

        [Test]
        public void startLoop()
        {
            string expected = "\nvoid loop()\n{\n";
            Assert.True(expected.Equals(Patterns.startLoop()));
        }

        [Test]
        public void endLoopNotNull()
        {
            Assert.IsNotNull(Patterns.endLoop());
        }

        [Test]
        public void endLoopNotEmpty()
        {
            Assert.IsNotEmpty(Patterns.endLoop());
        }

        [Test]
        public void endLoop()
        {
            string expected = "\tfor (;;) { }\n}\n\n";
            Assert.True(expected.Equals(Patterns.endLoop()));
        }

    }
}
