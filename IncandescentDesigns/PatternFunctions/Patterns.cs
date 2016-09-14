using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IncandescentDesigns.PatternFunctions
{
    public class Patterns
    {
        public Dictionary<string, string> patterns = new Dictionary<string, string>();

        /*
            I don't think these fields should stay public. Just adding get methods to a 
            private field didn't seem to work when I tried to access that field of the object from 
            the list in HomeController. There are various ways to handle this I'm just not sure which one would
            be best at this point, or if they would actually be better or not. 
        */
        public string thePattern { get; set; }
        public string theColor { get; set; }
        public string theDuration { get; set; }
        public string theSpeed { get; set; }
        public string theFileName { get; set; }
        public string theCubeSize { get; set; }

        public Patterns()
        {
            addFunctions();
        }

        public Patterns(string thePattern, string theColor, string theDuration, string theSpeed, string theFileName, string theCubeSize)
        {
            this.thePattern = thePattern;
            this.theColor = theColor;
            this.theDuration = theDuration;
            this.theSpeed = theSpeed;
            this.theFileName = theFileName;
            this.theCubeSize = theCubeSize;
        }

        private void addFunctions()
        {
            patterns.Add("twoDimLetters", 
                "void twoDimLetters(uint32_t color, unsigned long speed, unsigned long duration)\n" + 
                "{\n\tunsigned char x, y, z;\n\tunsigned long newDuration=duration;\n\tint count=0;\n\t" + 
                "while(count < duration)\n\t{\n\t\tRb.drawChar(0x48, 0, 1, color);\n\t\tdelay(speed);\n\t\t" + 
                "Rb.blankDisplay();\n\t\tRb.drawChar(0x65, 0, 1, color);\n\t\tdelay(speed);\n\t\tRb.blankDisplay();\n\t\t" + 
                "Rb.drawChar(0x6C, 0, 1, color);\n\t\tdelay(speed);\n\t\tRb.blankDisplay();\n\t\t" + 
                "Rb.drawChar(0x6C, 1, 1, color);\n\t\tdelay(speed);\n\t\tRb.blankDisplay();\n\t\t" + 
                "Rb.drawChar(0x6F, 0, 1, color);\n\t\tdelay(speed);\n\t\tRb.blankDisplay();\n\t\tcount++;\n\t}\n}\n\n");
            patterns.Add("randomColors", 
                "void randomColors(uint32_t color, unsigned long speed, unsigned long duration)\n{\n\tunsigned int z, x, y;\n\t" + 
                "unsigned long newDuration=duration;\n\tint count=0;\n\twhile (count < duration)\n\t{\n\t\t" + 
                "for (z = 0; z < 4; z++)\n\t\t{\n\t\t\tfor (x = 0; x < 4; x++)\n\t\t\t{\n\t\t\t\tfor (y = 0; y < 4; y++)\n\t" + 
                "\t\t\t{\n\t\t\t\t\tRb.setPixelZXY(z, x, y, random(0xFFFFFF));\n\t\t\t\t}\n\t\t\t}\n\t\t}\n\t\t" + 
                "delay(speed);\n\t\tRb.blankDisplay();\n\t\tcount++;\n\t}\n}\n\n");
            patterns.Add("sineFunction", 
                "void sineFunction(uint32_t color, unsigned long speed, unsigned long duration)\n{\n\t" + 
                "unsigned long newDuration=duration;\n\tint count=0;\n\tint size = 4;\n\t" + 
                "double phase[16] = {0, PI / 6, PI / 4, PI / 3, PI / 2, (2 * PI) / 3, (3 * PI) / 4, (5 * PI) / 6, PI, (7 * PI) / 6," + 
                " (5 * PI) / 4, (4 * PI) / 3, (3 * PI) / 2, (5 * PI) / 3, (7 * PI) / 4, (11 * PI) / 6};\n\tdouble Z;\n\t" + 
                "\n\tint phaseCount = 0;\n\twhile (count < duration)\n\t{\n\t\tfor (int x = 0; x<size; x++)\n\t\t" + 
                "{\n\t\t\tfor (int y = 0; y<size; y++)\n\t\t\t{\n\t\t\t\tZ = sin(phase[phaseCount] + sqrt(pow(sineMap(x, 0, size - 1, -PI, PI), 2)" +
                " + pow(sineMap(y, 0, size - 1, -PI, PI), 2)));\n\t\t\t\tZ = round(sineMap(Z, -1, 1, 0, size - 1));\n\t\t\t\tRb.setPixelZXY(x, y, Z, color); " +
                "\n\t\t\t}\n\t\t}\n\t\tdelay(speed);\n\t\tRb.blankDisplay();\n\t\tcount++;\n\t\tphaseCount++;\n\t}\n}\n\ndouble sineMap(double in, double inMin, double inMax, double outMin, double outMax)" + 
                "\n{\n\tdouble out;\n\tout = (in -inMin) / (inMax - inMin) * (outMax - outMin) + outMin;\n\treturn out;\n}\n\n");
            patterns.Add("wave", 
                "void wave(uint32_t color, unsigned long speed, unsigned long duration)\n{\n\tunsigned long newDuration=duration;" + 
                "\n\tint count=0;\n\tint size = 4;\n\tdouble Z;\n\twhile (count < duration)\n\t" + 
                "{\n\t\tfor (int x = 0; x<size; x++)\n\t\t{\n\t\t\tfor (int y = 0; y<size; y++)\n\t\t\t{\n\t\t\t\tZ = cos(waveMap" +
                "(x, 0, size - 1, -PI, PI)) + cos(waveMap(y, 0, size - 1, -PI, PI));\n\t\t\t\tZ = round(waveMap(Z, -1, 1, 0, size - 1)); \n\t\t\t\tRb.setPixelZXY(x, y, Z, color);" +
                "\n\t\t\t}\n\t\t}\n\t\tdelay(speed);\n\t\tRb.blankDisplay();\n\t\tcount++;\n\t\tphaseCount++;\n\t}\n}\n\ndouble waveMap(double in, double inMin, double inMax," + 
                " double outMin, double outMax)\n{\n\tdouble out;\n\tout = (in -inMin) / (inMax - inMin) * (outMax - outMin) + outMin;\n\treturn out;\n}\n\n");
            patterns.Add("flashZY",
                "void flashZY(uint32_t color, unsigned long speed, unsigned long duration)" +
                "\n{" +
                "\n\tunsigned long newDuration = duration;" +
                "\n\tint count = 0;" +
                "\n\tunsigned int z, x, y, level;" +
                "\n\tx = x = y = level = 0;" +
                "\n\twhile (count < duration)" +
                "\n\t{" +
                    "\n\t\twhile (level < 4)" +
                    "\n\t\t{" +
                        "\n\t\t\tfor (z = 0; z < 4; z++)" +
                        "\n\t\t\t{" +
                            "\n\t\t\t\tfor (y = 0; y < 4; y++)" +
                            "\n\t\t\t\t{" +
                                "\n\t\t\t\t\tRb.setPixelZXY(z, level, y, color);" +
                            "\n\t\t\t\t}" +
                        "\n\t\t\t}" +
                        "\n\t\t\tdelay(speed);" +
                        "\n\t\t\tRb.blankDisplay();" +
                        "\n\t\t\tlevel++;" +
                    "\n\t\t}" +
                    "\n\t\tcount++;" +
                    "\n\t\tlevel = 0;" +
                "\n\t}" +
            "\n}\n");
            patterns.Add("twoDimShapes", 
                "void twoDimShapes(uint32_t color, unsigned long speed, unsigned long duration)\n{\n\tunsigned int x, y;\n" + 
                "\tunsigned long newDuration=duration;\n\tint count=0;\n\twhile(count < duration)\n\t{\n\t\tfor (x = 0; x <= 8; x++)" + 
                "\n\t\t{\n\t\t\tfor (y = 0; y <= 8; y++)\n\t\t\t{\n\t\t\t\tRb.setPixelXY(x, y, color);\n\t\t\t}\n\t\t}\n" + 
                "\t\tdelay(speed);\n\t\tRb.blankDisplay(); //Clear the LEDs (make all blank)\n\t\tRb.drawCircle(4, 4, 2, color);" + 
                " // draw a circle of radius 2 at (4,4).\n\t\tdelay(speed);\n\t\tRb.blankDisplay();\n\t\tRb.fillCircle(4, 4, 2, color);" + 
                " // draw a circle of radius 2 at (4,4).\n\t\tdelay(speed);\n\t\tRb.blankDisplay();\n\t\tcount++;\n\t}\n}\n\n");
            patterns.Add("falling",
                "void falling(uint32_t color, unsigned long speed, unsigned long duration)" +
                "\n{" +
                "\n\tint x, y;" +
                "\n\tunsigned long newDuration = duration;" +
                "\n\tint count = 0;" +
                "\n\twhile (count < duration)" +
                "\n\t{" +
                    "\n\t\tfor (x = 0; x < 4; x++)" +
                    "\n\t\t{" +
                        "\n\t\t\tfor (y = 0; y < 4; y++)" +
                        "\n\t\t\t{" +
                            "\n\t\t\t\tRb.setPixelZXY(0, x, y, color);" +
                        "\n\t\t\t}" +
                        "\n\t\t\tdelay(speed);" +
                        "\n\t\t\tRb.blankDisplay();" +
                        "\n\t\t\tfor (y = 0; y < 4; y++)" +
                        "\n\t\t\t{" +
                            "\n\t\t\t\tRb.setPixelZXY(1, x, y, color); //uses 24bit RGB color Code" +
                        "\n\t\t\t}" +
                        "\n\t\t\tdelay(speed);" +
                        "\n\t\t\tRb.blankDisplay();" +
                        "\n\t\t\tfor (y = 0; y < 4; y++)" +
                        "\n\t\t\t{" +
                            "\n\t\t\t\tRb.setPixelZXY(2, x, y, color); //uses 24bit RGB color Code" +
                        "\n\t\t\t}" +
                        "\n\t\t\tdelay(speed);" +
                        "\n\t\t\tRb.blankDisplay();" +
                        "\n\t\t\tfor (y = 0; y < 4; y++)" +
                        "\n\t\t\t{" +
                            "\n\t\t\t\tRb.setPixelZXY(3, x, y, color); //uses 24bit RGB color Code" +
                        "\n\t\t\t}" +
                        "\n\t\t\tdelay(speed);" +
                        "\n\t\t\tRb.blankDisplay();" +
                    "\n\t\t}" +
                    "\n\t\tcount++;" +
                "\n\t\t}" +
            "\n}\n" 
            );
            patterns.Add("twoDimFalling",
              "void twoDimFalling(uint32_t color, unsigned long speed, unsigned long duration)" +
              "\n{" +
                "\n\tunsigned long newDuration = duration;" +
                "\n\tint count = 0;" +
                "\n\twhile (count < duration)" +
                "\n\t{" +
                    "\n\t\tfor (int i = 7; i >= 0; i--)" +
                    "\n\t\t{" +
                        "\n\t\t\tRb.drawHorizontalLine(0, i, 8, color);" +
                        "\n\t\t\tdelay(speed);" +
                        "\n\t\t\tRb.blankDisplay();" +
                    "\n\t\t}" +
                    "\n\t\tfor (int i = 0; i < 7; i++)" +
                    "\n\t\t{" +
                        "\n\t\t\tRb.drawHorizontalLine(0, i, 8, color);" +
                        "\n\t\t\tdelay(speed);" +
                        "\n\t\t\tRb.blankDisplay();" +
                    "\n\t\t}" +
                    "\n\t\tfor (int i = 7; i >= 0; i--)" +
                    "\n\t\t{" +
                        "\n\t\t\tRb.drawVerticalLine(i, 0, 8, color);" +
                        "\n\t\t\tdelay(speed);" +
                        "\n\t\t\tRb.blankDisplay();" +
                    "\n\t\t}" +
                    "\n\t\tfor (int i = 0; i < 7; i++)" +
                    "\n\t\t{" +
                        "\n\t\t\tRb.drawVerticalLine(i, 0, 8, color);" +
                        "\n\t\t\tdelay(speed);" +
                        "\n\t\t\tRb.blankDisplay();" +
                    "\n\t\t}" +
                    "\n\t\tcount++;" +
                "\n\t}" +
                "\n\tRb.blankDisplay();" +
            "\n}\n");
            patterns.Add("flashingSquares",
            "void flashingSquares(uint32_t color, unsigned long speed, unsigned long duration)" +
            "\n{" +
                "\n\tint x, y;" +
                "\n\tunsigned long newDuration = duration;" +
                "\n\tint count = 0;" +
                "\n\twhile (count < duration)" +
                "\n\t{" +
                    "\n\t\tRb.setPixelZXY(0, 0, 0, color);" +
                    "\n\t\tRb.setPixelZXY(0, 3, 0, color);" +
                    "\n\t\tRb.setPixelZXY(0, 0, 3, color);" +
                    "\n\t\tRb.setPixelZXY(0, 3, 3, color);" +
                    "\n\t\tdelay(speed);" +
                    "\n\t\tRb.blankDisplay();" +
                    "\n\t\tRb.setPixelZXY(3, 0, 3, color);" +
                    "\n\t\tRb.setPixelZXY(3, 0, 0, color);" +
                    "\n\t\tRb.setPixelZXY(3, 3, 3, color);" +
                    "\n\t\tRb.setPixelZXY(3, 3, 0, color);" +
                    "\n\t\tdelay(speed);" +
                    "\n\t\tRb.blankDisplay();" +
                    "\n\t\tcount++;" +
                "\n\t}" +
            "\n}\n");
            patterns.Add("twoDimFace",
            "void twoDimFace(uint32_t color, unsigned long speed, unsigned long duration)" +
            "\n{" +
                "\n\tint size = 4;" +
                "\n\tdouble Z;" +
                "\n\tunsigned long newDuration = duration;" +
                "\n\tint count = 0;" +
                "\n\twhile (count < duration)" +
                "\n\t{" +
                    "\n\t\tRb.setPixelXY(2, 4, color);" +
                    "\n\t\tRb.setPixelXY(5, 4, color);" +
                    "\n\t\tRb.drawHorizontalLine(2, 2, 4, color);" +
                    "\n\t\tdelay(speed);" +
                    "\n\t\tcount++;" +
                "\n\t}" +
                "\n\tRb.blankDisplay();" +
            "\n}\n");
        }

        //initialization functions

        public static string initializeMatrix()
        {
            string initializeMatrix = "#include <Rainbowduino.h>\n#include <stdlib.h>\n\nvoid setup()\n{\n\tRb.init();\n}\n";
            return initializeMatrix;
        }

        public static string startLoop()
        {
            string startLoop = "\nvoid loop()\n{\n";
            return startLoop;
        }

        public static string endLoop()
        {
            string endLoop = "\tfor (;;) { }\n}\n\n";
            return endLoop;
        }

        //default customization values

        public static string getDefaultSpeed()
        {
            string defaultSpeed = "0.0005";
            return defaultSpeed;
        }

        public static string getDefaultColor()
        {
            string defaultColor = "#FF0000";
            return defaultColor;
        }

        public static string getDefaultDuration()
        {
            string defaultDuration = "5";
            return defaultDuration;
        }

        public static string getDefaultPattern()
        {
            string defaultPattern = "twoDimLetters";
            return defaultPattern;
        }

        //customization functions

        public static string color(string color)
        {
            if (string.IsNullOrWhiteSpace(color))
            {
                string colorDefault = Patterns.getDefaultColor();
                return colorDefault;
            }
            else
            {
                string customColor = color;
                return customColor;
            }
        }

        public static string speed(string speed)
        {
            if (string.IsNullOrWhiteSpace(speed))
            {
                string speedDefault = Patterns.getDefaultSpeed();
                return speedDefault;
            }
            else
            {
                string customSpeed = speed;
                return customSpeed;
            }
        }

        public static string duration(string duration)
        {
            if (string.IsNullOrWhiteSpace(duration))
            {
                string durationDefault = Patterns.getDefaultDuration();
                return durationDefault;
            }
            else
            {
                string customDuration = duration;
                return customDuration;
            }
        }

       public static bool checkSpeed(string speed)
       {
            float checkSpeed = 0;
            bool canConvert = false;
            canConvert = float.TryParse(speed, out checkSpeed);
            if (canConvert)
            {
                speed = convertSpeed(speed);
                canConvert = true;
            }
            return canConvert;
        }

        public static string convertSpeed(string speed)
        {
            float convertSpeed = float.Parse(speed) * 1000000;
            return speed = "" + convertSpeed;
        }

        public static bool checkDuration(string duration)
        {
            int checkDuration = 0;
            bool canConvert = false;
            canConvert = int.TryParse(duration, out checkDuration);
            if (canConvert)
            {
                canConvert = true;
            }
            return canConvert;
        }



    }
}