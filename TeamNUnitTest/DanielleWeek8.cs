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
    class DanielleWeek8
    {
        [Test]
        public void TestPatternListNotNull()
        {
            Patterns thePatterns = new Patterns();
            Assert.IsNotNull(thePatterns.patterns);
        }

        [Test]
        public void TestPatternListCount()
        {
            //Patterns thePatterns = new Patterns();
            //Assert.True(thePatterns.patterns.Count == 6);
        }

        [Test]
        public void TestPatternListKeyValuePairs()
        {
        //   Patterns thePatterns = new Patterns();
        //    Assert.True(thePatterns.patterns.ContainsKey("twoDimLetters") && thePatterns.patterns["twoDimLetters"].Equals("void twoDimLetters(uint32_t color, unsigned long speed, unsigned long duration)\n{\n\tunsigned char x, y, z;\n\tunsigned long newDuration=duration;\n\tint count=0;\n\twhile(count < duration)\n\t{\n\t\tRb.drawChar(0x48, 0, 1, color);\n\t\tdelay(speed);\n\t\tRb.blankDisplay();\n\t\tRb.drawChar(0x65, 0, 1, color);\n\t\tdelay(speed);\n\t\tRb.blankDisplay();\n\t\tRb.drawChar(0x6C, 0, 1, color);\n\t\tdelay(speed);\n\t\tRb.blankDisplay();\n\t\tRb.drawChar(0x6C, 1, 1, color);\n\t\tdelay(speed);\n\t\tRb.blankDisplay();\n\t\tRb.drawChar(0x6F, 0, 1, color);\n\t\tdelay(speed);\n\t\tRb.blankDisplay();\n\t\tcount++;\n\t}\n}\n\n"));
        //    Assert.True(thePatterns.patterns.ContainsKey("randomColors") && thePatterns.patterns["randomColors"].Equals("void randomColors(uint32_t color, unsigned long speed, unsigned long duration)\n{\n\tunsigned int z, x, y;\n\tunsigned long newDuration=duration;\n\tint count=0;\n\twhile (count < duration)\n\t{\n\t\tfor (z = 0; z < 4; z++)\n\t\t{\n\t\t\tfor (x = 0; x < 4; x++)\n\t\t\t{\n\t\t\t\tfor (y = 0; y < 4; y++)\n\t\t\t\t{\n\t\t\t\t\tRb.setPixelZXY(z, x, y, random(0xFF));\n\t\t\t\t}\n\t\t\t}\n\t\t}\n\t\tdelay(speed);\n\t\tRb.blankDisplay(); //Clear the LEDs (make all blank)\n\t}\n}\n\n"));
        //    Assert.True(thePatterns.patterns.ContainsKey("sineFunction") && thePatterns.patterns["sineFunction"].Equals("void sineFunction(uint32_t color, unsigned long speed, unsigned long duration)\n{\n\tunsigned long newDuration=duration;\n\tint count=0;\n\tint size = 4;\n\tdouble phase[16] = {0, PI / 6, PI / 4, PI / 3, PI / 2, (2 * PI) / 3, (3 * PI) / 4, (5 * PI) / 6, PI, (7 * PI) / 6, (5 * PI) / 4, (4 * PI) / 3, (3 * PI) / 2, (5 * PI) / 3, (7 * PI) / 4, (11 * PI) / 6};\n\tdouble Z;\n\tint cubeArray[3][3][3];\n\tint phaseCount = 0;\n\twhile (count < duration)\n\t{\n\t\tfor (int x = 0; x<size; x++)\n\t\t{\n\t\t\tfor (int y = 0; y<size; y++)\n\t\t\t{\n\t\t\t\tZ = sin(phase[phaseCount] + sqrt(pow(sineMap(x, 0, size - 1, -PI, PI), 2) + pow(sineMap(y, 0, size - 1, -PI, PI), 2)));\n\t\t\t\tZ = round(sineMap(Z, -1, 1, 0, size - 1));\n\t\t\t}\n\t\t}\n\tcubeArray[3][3][3] = {};\n\tphaseCount++;\n\t}\n}\n\ndouble sineMap(double in, double inMin, double inMax, double outMin, double outMax)\n{\n\tdouble out;\n\tout = (in -inMin) / (inMax - inMin) * (outMax - outMin) + outMin;\n\treturn out;\n}\n\n"));
        //    Assert.True(thePatterns.patterns.ContainsKey("wave") && thePatterns.patterns["wave"].Equals("void wave(uint32_t color, unsigned long speed, unsigned long duration)\n{\n\tunsigned long newDuration=duration;\n\tint count=0;\n\tint size = 4;\n\tdouble Z;\n\tint cubeArray[size][size][size];\n\twhile (count < duration)\n\t{\n\t\tfor (int x = 0; x<size; x++)\n\t\t{\n\t\t\tfor (int y = 0; y<size; y++)\n\t\t\t{\n\t\t\t\tZ = cos(waveMap(x, 0, size - 1, -PI, PI)) + cos(waveMap(y, 0, size - 1, -PI, PI));\n\t\t\t\tZ = round(waveMap(Z, -1, 1, 0, size - 1));\n\t\t\t}\n\t\t}\n\t\tcubeArray[size][size][size] = {};\n\t}\n}\n\ndouble waveMap(double in, double inMin, double inMax, double outMin, double outMax)\n{\n\tdouble out;\n\tout = (in -inMin) / (inMax - inMin) * (outMax - outMin) + outMin;\n\treturn out;\n}\n\n"));
        //    Assert.True(thePatterns.patterns.ContainsKey("flashZYAndXY") && thePatterns.patterns["flashZYAndXY"].Equals("void flashZYAndXY(uint32_t color, unsigned long speed, unsigned long duration)\n{\n\tunsigned long newDuration=duration;\n\tint count=0;\n\tunsigned int z, x, y, level;\n\tz = x = y = level = 0;\n\n\t//set each z-y plane of the cube a random color for 3 seconds\n\twhile (count < duration)\n\t{\n\t\twhile (level < 4)\n\t\t{\n\t\t\tfor (z = 0; z < 4; z++)\n\t\t\t{\n\t\t\t\tfor (y = 0; y < 4; y++)\n\t\t\t\t{\n\t\t\t\t\tRb.setPixelZXY(z, level, y, random(0xFF), random(0xFF), random(0xFF));\n\t\t\t\t}\n\t\t\t}\n\n\t\t\t//delay for 3 seconds\n\t\t\tdelay(speed);\n\t\t\t//clear the LED's\n\t\t\tRb.blankDisplay();\n\t\t\tlevel++;\n\t\t\t\n\n\t\t\t//set each x-y plane of the cube a random color for 3 seconds\n\t\t\twhile (level < 4)\n\t\t\t{\n\t\t\t\tfor (x = 0; x < 4; x++)\n\t\t\t\t{\n\t\t\t\t\tfor (y = 0; y < 4; y++)\n\t\t\t\t\t{\n\t\t\t\t\t\tRb.setPixelZXY(level, x, y, random(0xFF), random(0xFF), random(0xFF));\n\t\t\t\t\t}\n\t\t\t}\n\n\t\t\t//delay for 3 seconds\n\t\t\tdelay(speed);\n\t\t\t//clear the LED's\n\t\t\tRb.blankDisplay();\n\t\t\tlevel++;\n\t\t}\n\t}\n}\n\n"));
        //    Assert.True(thePatterns.patterns.ContainsKey("twoDimShapes") && thePatterns.patterns["twoDimShapes"].Equals("void twoDimShapes(uint32_t color, unsigned long speed, unsigned long duration)\n{\n\tunsigned int x, y;\n\tunsigned long newDuration=duration;\n\tint count=0;\n\twhile(count < duration)\n\t{\n\t\tfor (x = 0; x <= 8; x++)\n\t\t{\n\t\t\tfor (y = 0; y <= 8; y++)\n\t\t\t{\n\t\t\t\tRb.setPixelXY(x, y, color);\n\t\t\t}\n\t\t}\n\t\tdelay(speed);\n\t\tRb.blankDisplay(); //Clear the LEDs (make all blank)\n\t\tRb.drawCircle(4, 4, 2, color); // draw a circle of radius 2 at (4,4).\n\t\tdelay(speed);\n\t\tRb.blankDisplay();\n\t\tRb.fillCircle(4, 4, 2, color); // draw a circle of radius 2 at (4,4).\n\t\tdelay(speed);\n\t\tRb.blankDisplay();\n\t\tcount++;\n\t}\n}\n\n"));
        }
    }
}
