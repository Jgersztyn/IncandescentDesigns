using System;
using NUnit.Framework;
using IncandescentDesigns.Controllers;
using System.Web.Mvc;
using IncandescentDesigns.PatternFunctions;
using System.Collections.Specialized;

namespace TeamNUnitTest
{
    [TestFixture]
    public class JasonUnitTests13
    {

        [Test]
        public void TestFileDownloadName()
        {
            PremadeProgramController ppc = new PremadeProgramController();

            byte[] byteArray = { 2, 32, 242, 255, 43, 1, 5, 77, 81, 0 };
            string name = "Test_File.txt";
            FileResult result = ppc.DownloadSomething(byteArray, name);

            Assert.That(result.FileDownloadName, Is.EqualTo(name));
        }

        [Test]
        public void TestFileDownloadNameLower()
        {
            PremadeProgramController ppc = new PremadeProgramController();

            byte[] byteArray = { 244, 122, 42, 124, 3, 112, 214, 88, 74, 1, 31, 233 };
            string name = "Test_File.txt";
            FileResult result = ppc.DownloadSomething(byteArray, name.ToLower());

            Assert.That(result.FileDownloadName, !Is.EqualTo(name));
        }

        [Test]
        public void TestFileExtensionChanged()
        {
            PremadeProgramController ppc = new PremadeProgramController();

            byte[] byteArray = { 32, 242, 225, 113, 4, 124, 124, 222, 213, 1, 5, 77, 81, 66, 0, 255 };
            string name = "Test_File.txt";
            FileResult result = ppc.DownloadSomething(byteArray, name);

            result = ppc.changeFileExtension(result, ".cpp");

            string extension = result.FileDownloadName.Substring(result.FileDownloadName.Length - 4);

            Assert.That(extension, Is.EqualTo(".cpp"));
        }

        [Test]
        public void TestFileExtensionNotChanged()
        {
            PremadeProgramController ppc = new PremadeProgramController();

            byte[] byteArray = { 2, 32, 242, 255, 43, 2, 25, 144, 77, 81, 0 };
            string name = "Test_File.txt";
            FileResult result = ppc.DownloadSomething(byteArray, name);

            result = ppc.changeFileExtension(result, ".ino");

            string extension = result.FileDownloadName.Substring(result.FileDownloadName.Length - 4);

            Assert.That(extension, !Is.EqualTo(".cpp"));
        }

        [Test]
        public void TestFileDownloadNotDuplicate()
        {
            PremadeProgramController ppc = new PremadeProgramController();

            byte[] byteArray = { 32, 242, 225, 113, 4, 124, 124, 222, 213, 1, 5, 77, 81, 66, 0, 255 };
            string name = "Test_File.txt";
            FileResult result = ppc.DownloadSomething(byteArray, name);

            byteArray[10] = 0;
            FileResult otherResult = ppc.DownloadSomething(byteArray, name);

            Assert.That(result, !Is.EqualTo(otherResult));
        }
    }
}
