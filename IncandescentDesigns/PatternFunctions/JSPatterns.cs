using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncandescentDesigns.PatternFunctions
{
    public class JSPatterns
    {
        public Dictionary<string, string> patternsJS = new Dictionary<string, string>();

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

        public JSPatterns()
        {
            addFunctions();
        }

        public JSPatterns(string thePattern, string theColor, string theDuration, string theSpeed, string theFileName, string theCubeSize)
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
            patternsJS.Add("randomColors", "if ((newTime - oldTime) < 5) {for (z = -2; z < 2; z++){for (x = -2; x < 2; x++){" + 
                    "for (y = -2; y < 2; y++){setPixelZXY(z, x, y, randomColor);count = 0;randColors();" + 
                    "newTime = timer.getElapsedTime();renderer.render(scene, camera);}}}}else {if ((count % 20) == 0) {" +
                    "color = !color;}for (z = -2; z< 2; z++) {for (x = -2; x< 2; x++) {for (y = -2; y< 2; y++) {if (color) {" +
                    "setPixelZXY(z, x, y, 0x800080);count = (count + 1);newTime = timer.getElapsedTime();renderer.render(scene, camera);" + 
                    "}else {setPixelZXY(z, x, y, 0xC4D0DB)count = (count + 1);newTime = timer.getElapsedTime();renderer.render(scene, camera);" +
                    "}}}}}");
            patternsJS.Add("flashColor", "if ((count % 200) == 0) {color = !color;}for (z = 0; z< 4; z++) {for (x = 0; x< 4; x++) {" +
                    "for (y = 0; y< 4; y++) {if (color) {setPixelZXY(z, x, y, 0x800080);count = (count + 1);}else {setPixelZXY(z, x, y, 0xC4D0DB)" +
                    "count = (count + 1);}}}}");
        }

        //initialization functions

        public static string initializeMatrix()
        {
            string initializeMatrix = "";
            return initializeMatrix;
        }

        public static string startLoop()
        {
            string startLoop = "";
            return startLoop;
        }

        public static string endLoop()
        {
            string endLoop = "";
            return endLoop;
        }

        //default customization values

        public static string getDefaultSpeed()
        {
            string defaultSpeed = "500";
            return defaultSpeed;
        }

        public static string getDefaultColor()
        {
            string defaultColor = "FF0000";
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
            return null;
        }

        public static string speed(string speed)
        {
            return null;
        }

        public static string duration(string duration)
        {
            return null;
        }
    }
}