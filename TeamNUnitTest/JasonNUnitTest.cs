using System;
using NUnit.Framework;
using IncandescentDesigns.Controllers;
using System.Web.Mvc;
using IncandescentDesigns.PatternFunctions;
using System.Collections.Specialized;

namespace TeamNUnitTest
{
    [TestFixture]
    public class UnitTest1
    {

        //[Test]
        //public void JasonTestButtonActionEvent()
        //{
        //HomeController c = new HomeController();

        //string createButton = "Create";

        //NameValueCollection formData = new NameValueCollection();
        //formData.Add("CubeSize", "8x8");
        //formData.Add("Pattern", "twoDimLetters");
        //formData.Add("color", "13524E");
        //formData.Add("Speed", "10");
        //formData.Add("Duration", "55");
        //formData.Add("FileName", "myFile.ino");
        //formData.Add("submitButton", null);
        //formData.Add("AddedPatterns", "twoDimLetters");

        //FormCollection form = new FormCollection(formData);

        //form.Set("CubeSize", "8x8");
        //form.Set("Pattern", "twoDimLetters");
        //form.Set("color", "13524E");
        //form.Set("Speed", "10");
        //form.Set("Duration", "55");
        //form.Set("FileName", "myFile.ino");
        //form.Set("submitButton", null);
        //form.Set("AddedPatterns", "twoDimLetters");

        //c.ButtonAction(createButton, form);

        //var result = c.ButtonAction(createButton, new FormCollection(formData)) as ViewResult;
        //Assert.That(result, Is.EqualTo("Create"));


        //Assert.IsInstanceOf(typeof(CreateModels), result.ViewData.Model);
        //var myModel = result.ViewData.Model as CreateModels;
        //Assert.That(myModel.Name, Is.EqualTo("Hello World"));
        //}

        [Test]
        public void ViewReturnedNotCreateTest()
        {
            HomeController c = new HomeController();

            string createButton = "Submit";

            NameValueCollection formData = new NameValueCollection();
            formData.Add("CubeSize", "8x8");
            formData.Add("Pattern", "twoDimLetters");
            formData.Add("color", "13524E");
            formData.Add("Speed", "10");
            formData.Add("Duration", "55");
            formData.Add("FileName", "myFile.ino");
            formData.Add("submitButton", null);
            formData.Add("AddedPatterns", "twoDimLetters");

            FormCollection form = new FormCollection(formData);

            var result = c.ButtonAction(createButton, new FormCollection(formData)) as ViewResult;
            Assert.That(result, !Is.EqualTo("Create"));
        }

        [Test]
        public void DefaultColorTest()
        {
            //string s = Patterns.getDefaultColor();
            //Assert.That(s, Is.EqualTo("FF0000"));
        }

        [Test]
        public void DefaultPatternTest()
        {
            string s = Patterns.getDefaultPattern();
            Assert.That(s, Is.EqualTo("twoDimLetters"));
        }

        [Test]
        public void DefaultSpeedTest()
        {
            //string s = Patterns.getDefaultSpeed();
            //Assert.That(s, Is.EqualTo("500"));
        }

        [Test]
        public void DefaultDurationTest()
        {
            string s = Patterns.getDefaultDuration();
            Assert.That(s, Is.EqualTo("5"));
        }

        [Test]
        public void PatternsNonDefaultValuesTest()
        {
            Patterns pattern = new Patterns("wave", "FED414", "25", "120", "myfile.ino", "4x4x4");

            string color = pattern.theColor;
            Assert.That(color, Is.EqualTo("FED414"));

            string pat = pattern.thePattern;
            Assert.That(pat, Is.EqualTo("wave"));

            //Patterns patternTwo = new Patterns("sineFunction", "FED414", "25", "450", "myfile.ino", "4x4x4");
            //Assert.That(pattern, !Is.EqualTo(patternTwo));
        }
    }
}