using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using IncandescentDesigns.Models;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using IncandescentDesigns.DAL;
using System.IO;
using IncandescentDesigns.PatternFunctions;
using Microsoft.AspNet.Identity;
using System.Web.Security;

namespace IncandescentDesigns.Controllers
{
    public class HomeController : Controller
    {
        private IncandescentDesignsContext db = new IncandescentDesignsContext();
        public static List<Patterns> codeGenPatternList = new List<Patterns>();
        public static List<int> newSequenceList = new List<int>();
        Patterns patternCode = new Patterns();


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        //gets the news feed from the database and returns the partial from the model
        //renders as an action
        public ActionResult GetNewsFeed()
        {
            //get news feed data from db and display only the most recent item
            if (db.News != null)
            {
                var model = db.News.OrderByDescending(x => x.PostID).Take(1).ToList();
                return PartialView("_Feed", model);
            }
            //nothing in the news feed...
            else
            {
                return PartialView("_Feed");
            }
        }

        /*
        * POST: Contact
        * Used to handle smtp requests and send emails to the Genietech staff.
        * Emails are created upon a user writing a message in the Contact form and submitting it.
        */
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(ContactModels model)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} {1} ({2})</p><p>Message:</p><p>{3}</p>";
                var message = new MailMessage();

                //get the email address of the currently logged in user
                //var email = User.Identity.Name;

                //this may actually send the email to the currently logged in user
                //message.To.Add(new MailAddress(email.ToUpper()));

                //the email will be sent to this address
                message.To.Add(new MailAddress("genietechwou@gmail.com"));
                //this is the receiving email address (though it may go to genietech...)
                message.From = new MailAddress(model.FromEmail.ToString());
                //title for the email being sent
                message.Subject = model.Title;
                //format the message body based on form data
                message.Body = string.Format(body, model.FirstName, model.LastName, model.FromEmail, model.Comment);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    //network credentials for the email administrator email address
                    var credential = new NetworkCredential
                    {
                        UserName = "genietechwou@gmail.com",
                        Password = "1q2w#E$R"
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com	";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;

                    //sends the actual message
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("About");
                }
            }
            return View(model);
        }

        // GET: Contact
        [Authorize]
        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult GetPatterns(string cubesize)
        {
            if (cubesize == "8x8")
            {
                var patterns = new List<SelectListItem>
                {
                    new SelectListItem {Text = "Letters: Hello", Value = "twoDimLetters"},
                    new SelectListItem {Text = "Circles and Squares", Value = "twoDimShapes"},
                    new SelectListItem {Text = "Falling", Value = "twoDimFalling"},
                    new SelectListItem {Text = "Face", Value = "twoDimFace"}
                };
                return Json(patterns, JsonRequestBehavior.AllowGet);
            }
            if (cubesize == "4x4x4")
            {
                var patterns = new List<SelectListItem>
                {
                    new SelectListItem {Text = "Random Colors", Value = "randomColors"},
                    new SelectListItem {Text = "Sine Function", Value = "sineFunction"},
                    new SelectListItem {Text = "Wave", Value = "wave"},
                    new SelectListItem {Text = "Flash", Value = "flashZY"},
                    new SelectListItem {Text = "Falling", Value = "falling"},
                    new SelectListItem {Text = "Flashing Squares", Value = "flashingSquares"}
                };
                return Json(patterns, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        public ActionResult Feed()
        {
            return View();
        }

        [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
        public ActionResult Create()
        {
            Session.Clear();
            CreateModels model = new CreateModels();
            model.FileName = "";
            model.Color = "";
            model.Pattern = "";
            model.Speed = "";
            model.Duration = Patterns.getDefaultDuration();
            model.CubeSize = "";
            model.AddedPatterns = "";
            ViewBag.disableFileName = false;
            ViewBag.disableCubeSize = false;
            codeGenPatternList.Clear();
            buildSequenceList();

            return View();
        }

        /*
        * Handles the download of a user stored file
        * Note that this function does not work properly, nor does it need to yet
        */
        public ActionResult Download()
        {
            return View();
        }
        //public FileResult Download(int? id)
        //{
        //    StoredFileModel stored = db.StoredFiles.Find(id); //not pulling the ID here
        //    return File(stored.inoFile, System.Net.Mime.MediaTypeNames.Application.Octet, stored.FileName);
        //}

        public ActionResult MacTutorial()
        {
            return View();
        }

        public ActionResult WindowsTutorial()
        {
            return View();
        }

        public ActionResult LinuxTutorial()
        {
            return View();
        }

        public ActionResult Tutorial()
        {
            return View();
        }

        /* Listen for the press of the button on the Pattern Creation page
           Jumps to the appropriate view based on which button is pushed
        */
        [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
        public ActionResult ButtonAction(string submitButton, FormCollection form)
        {
            CreateModels model = new CreateModels();

            switch (submitButton)
            {
                //user wants to create the .ino file as it is now, using this current pattern
                case "Create":

                    //if no patterns are currently present, then no file should be generated give the user an error
                    if (string.IsNullOrWhiteSpace(Request.Form["AddedPatterns"]))
                    {
                        ModelState.AddModelError("", "Please add at least one pattern before trying to create a file.");
                        codeGenPatternList.Clear();
                        buildSequenceList();
                        model.FileName = Request.Form["FileName"];
                        model.Color = Request.Form["Color"];
                        model.Pattern = Request.Form["Pattern"];
                        model.Speed = Request.Form["Speed"];
                        model.Duration = Patterns.getDefaultDuration();
                        model.CubeSize = Request.Form["CubeSize"];
                        model.AddedPatterns = "";
                        ViewBag.disableFileName = false;
                        ViewBag.disableCubeSize = false;
                        return View("Create", model);
                    } if (Request.Form["CubeSize"].Equals("-- Select Cube Size --") || string.IsNullOrWhiteSpace(Request.Form["CubeSize"]) && codeGenPatternList.Count == 0)
                    {
                        ModelState.AddModelError("", "Please remember to select a cube size & a pattern.");
                        //codeGenPatternList.Clear();
                        buildSequenceList();
                        model.FileName = Request.Form["FileName"];
                        model.Color = Request.Form["Color"];
                        model.Pattern = Request.Form["Pattern"];
                        model.Speed = Request.Form["Speed"];
                        model.Duration = Patterns.getDefaultDuration();
                        model.CubeSize = Request.Form["CubeSize"];
                        model.AddedPatterns = "";
                        ViewBag.disableFileName = false;
                        ViewBag.disableCubeSize = false;
                        return View("Create", model);
                    } if ((Request.Form["Pattern"].Equals("-- Select Pattern --") || string.IsNullOrWhiteSpace(Request.Form["Pattern"])) && codeGenPatternList.Count == 0)
                    {
                        ModelState.AddModelError("", "Please remember to select a cube size & a pattern.");
                        //codeGenPatternList.Clear();
                        buildSequenceList();
                        model.CubeSize = Request.Form["CubeSize"];
                        model.FileName = Request.Form["FileName"];
                        model.Color = Request.Form["Color"];
                        model.Pattern = Request.Form["Pattern"];
                        model.Speed = Request.Form["Speed"];
                        model.Duration = Patterns.getDefaultDuration();
                        model.CubeSize = Request.Form["CubeSize"];
                        model.AddedPatterns = "";
                        ViewBag.disableFileName = false;
                        ViewBag.disableCubeSize = false;
                        //ViewBag.disableFileName = false;
                        //ViewBag.disableCubeSize = false;
                        return View("Create", model);
                    }
                    //This is the code which handles pattern creation
                    //CreatePattern() is only responsible for downloading the file
                    else
                    {
                        //return (CreatePattern(form));
                        model.AddedPatterns = "";

                        //get name of file
                        string fileName = codeGenPatternList.ElementAt(0).theFileName;

                        //change file extension to .ino
                        Session["FileName"] = Path.ChangeExtension(fileName, ".ino");
                        //change file extension to .js
                        Session["SimulatorFile"] = Path.ChangeExtension(fileName, ".js");

                        //create the file by looping through list of form data stored as Patterns objects
                        //the list data is used for the information to call the functions and to add
                        //the function code from the dictionary
                        string callFunctions = "";

                        //patterns
                        List<Patterns> ordered = (codeGenPatternList.Count >= 1) ? sortLists(codeGenPatternList) : ordered = new List<Patterns>(codeGenPatternList);

                        for (int i = 0; i < codeGenPatternList.Count; i++)
                        {
                            callFunctions += "\t" + ordered.ElementAt(i).thePattern.ToString() + "(" + "0x" + ordered.ElementAt(i).theColor.ToString()
                                + "," + ordered.ElementAt(i).theSpeed.ToString() + "," + ordered.ElementAt(i).theDuration.ToString() + ");\n";
                        }

                        //code to only add functionCode once with a HashSet!!!

                        string functionCode = "";
                        HashSet<String> duplicates = new HashSet<String>();
                        for (int i = 0; i < codeGenPatternList.Count; i++)
                        {
                            duplicates.Add(codeGenPatternList.ElementAt(i).thePattern.ToString());
                        }

                        for (int i = 0; i < duplicates.Count; i++)
                        {
                            functionCode += patternCode.patterns[duplicates.ElementAt(i)];
                        }

                        string fileData = Patterns.initializeMatrix() + Patterns.startLoop() + callFunctions + Patterns.endLoop() + functionCode;

                        //var byteArray = Encoding.ASCII.GetBytes(fileData);
                        //var stream = new MemoryStream(byteArray);

                        //clear the list of patterns from the form data
                        codeGenPatternList.Clear();
                        buildSequenceList();
                        model.AddedPatterns = "";

                        Session["TheFile"] = Encoding.ASCII.GetBytes(fileData);

                        return RedirectToAction("Success");
                    }

                //user wants to add this pattern to the list of patterns and then move to the next
                case "Add":
                   if (Request.Form["CubeSize"].Equals("-- Select Cube Size --") || string.IsNullOrWhiteSpace(Request.Form["CubeSize"]) && codeGenPatternList.Count == 0)
                   {
                       ModelState.AddModelError("", "Please remember to select a cube size & a pattern.");
                       //codeGenPatternList.Clear();
                       buildSequenceList();
                       model.FileName = Request.Form["FileName"];
                       model.Color = Request.Form["Color"];
                       model.Pattern = Request.Form["Pattern"];
                       model.Speed = Request.Form["Speed"];
                       model.Duration = Patterns.getDefaultDuration();
                       model.CubeSize = Request.Form["CubeSize"];
                       model.AddedPatterns = "";

                       if (codeGenPatternList.Count > 0) { ViewBag.disableFileName = true; } else { ViewBag.disableFileName = false; };
                       if (codeGenPatternList.Count > 0) { ViewBag.disableCubeSize = true; } else { ViewBag.disableCubeSize = false; };
                       return View("Create", model);          
                    }
                    if (Request.Form["Pattern"].Equals("-- Select Pattern --") || string.IsNullOrWhiteSpace(Request.Form["Pattern"]))
                    {
                        ModelState.AddModelError("", "Please remember to select a cube size & a pattern.");
                        //codeGenPatternList.Clear();
                        buildSequenceList();
                        model.FileName = Request.Form["FileName"];
                        model.Color = Request.Form["Color"];
                        model.Pattern = Request.Form["Pattern"];
                        model.Speed = Request.Form["Speed"];
                        model.Duration = Patterns.getDefaultDuration();
                        model.CubeSize = Request.Form["CubeSize"];
                        model.AddedPatterns = "";

                        if (codeGenPatternList.Count > 0) { ViewBag.disableFileName = true; } else { ViewBag.disableFileName = false; };
                        if (codeGenPatternList.Count > 0) { ViewBag.disableCubeSize = true; } else { ViewBag.disableCubeSize = false; };
                        return View("Create", model);
                    }
                    return (AddPattern(form));

                //this will remove the last pattern that was added to the textbox on the form
                case "Remove":
                    if (codeGenPatternList.Count == 0)
                    {
                        ModelState.AddModelError("", "Please add a pattern before trying to remove anything.");
                        //codeGenPatternList.Clear();
                        buildSequenceList();
                        model.FileName = Request.Form["FileName"];
                        model.Color = Request.Form["Color"];
                        model.Pattern = Request.Form["Pattern"];
                        model.Speed = Request.Form["Speed"];
                        model.Duration = Patterns.getDefaultDuration();
                        model.CubeSize = Request.Form["CubeSize"];
                        model.AddedPatterns = "";

                        if (codeGenPatternList.Count > 0) { ViewBag.disableFileName = true; } else { ViewBag.disableFileName = false; };
                        if (codeGenPatternList.Count > 0) { ViewBag.disableCubeSize = true; } else { ViewBag.disableCubeSize = false; };
                        return View("Create", model);
                    }
                    return (RemovePattern(form));

                //default back to this page if something weird happens
                default:
                    return RedirectToAction("Create");
            }
        }

        /*
            It is easy to customize a program based on pattern. If the user does not put in 
            a color value the default will be entered into the code where the function is called in the main loop. If a user puts
            in a value then the form input will be used as the parameter to the function in the file generated.  
        */

        /* 
            The "Sine" pattern may be a problem, as it generates a function called map()
            Each time the Sine pattern is added to the list, a new map function will be created
            Functions with the same name cannot occur twice in the Arduino file
            This can easily be addressed later on, but it should be its own seperate user story/bug

            For now I just changed it so that each pattern has a function like the sine pattern has 
            sineMap() and the wave pattern has waveMap()...not the best solution but will produce working
            code until we find something more appropriate.
        */

        //Function to download the user created file
        public ActionResult CreatePattern()
        {
            byte[] byteArray = (byte[])Session["TheFile"];
            string fileName = (string)Session["FileName"];

            //if (byteArray == null || byteArray.Length == 0)
            //{
            //    return View("Error");
            //}

            return File(byteArray, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        /*
        * Handles the upload of a user stored file immediately after creation
        */
        //[Authorize]
        public ActionResult SavePattern()
        {
            StoredFileModel stored = new StoredFileModel();
            FavoriteProgram favorites = new FavoriteProgram();

            if (ModelState.IsValid)
            {
                //get program file information
                stored.inoFile = (byte[])Session["TheFile"];
                stored.FileName = (string)Session["FileName"];

                //if (byteArray == null || byteArray.Length == 0)
                //{
                //    return View("Error");
                //}

                db.StoredFiles.Add(stored);
                db.SaveChanges(); //file has been saved

                //id of newly added file; somehow Entity Framework puts this here
                int storedFileId = stored.StoredFileId;

                //id of the currently loggedin user
                var loggedInId = User.Identity.GetUserId();

                //create the record in the favorite program table
                favorites.StoredFileId = storedFileId;
                favorites.UserId = loggedInId;

                //save the changes
                db.FavoritePrograms.Add(favorites);
                db.SaveChanges();

                //return the model view
                //return RedirectToAction("StoredFiles");

                //clear all values for this session
                Session.Clear();

                //redirect to the user's page after saving the file
                return RedirectToAction("FavoritePrograms" + "/" + loggedInId, "Profile");
            }
            //else
            //{
            //    return View("Error");
            //}

            return View(stored);
        }

        /*
        * Displays the user stored files in a table
        */
        public ActionResult StoredFiles()
        {
            return View(db.StoredFiles.ToList());
        }

        public ActionResult AddPattern(FormCollection form)
        {
            CreateModels model = new CreateModels();

            //request form data
            string fileName = Request.Form["FileName"];
            string color = Request.Form["Color"];
            string pattern = Request.Form["Pattern"];
            string speed = Request.Form["Speed"];
            string duration = Request.Form["Duration"];
            string patternsForBox = Request.Form["Pattern"];
            string cubeSize = Request.Form["CubeSize"];

            if (!Patterns.checkSpeed(speed)) { 
                ModelState.AddModelError("", "Please only input a decimal in the speed field.");
                buildSequenceList();
                model.FileName = Request.Form["FileName"];
                model.Color = Request.Form["Color"];
                model.Pattern = Request.Form["Pattern"];
                model.Speed = Request.Form["Speed"];
                model.Duration = Patterns.getDefaultDuration();
                model.CubeSize = Request.Form["CubeSize"];
                model.AddedPatterns = "";

                if (codeGenPatternList.Count > 0) { ViewBag.disableFileName = true; } else { ViewBag.disableFileName = false; };
                if (codeGenPatternList.Count > 0) { ViewBag.disableCubeSize = true; } else { ViewBag.disableCubeSize = false; };
                return View("Create", model);
            }

            if (!Patterns.checkDuration(duration))
            {
                ModelState.AddModelError("", "Please only input an int in the duration field.");
                buildSequenceList();
                model.FileName = Request.Form["FileName"];
                model.Color = Request.Form["Color"];
                model.Pattern = Request.Form["Pattern"];
                model.Speed = Request.Form["Speed"];
                model.Duration = Patterns.getDefaultDuration();
                model.CubeSize = Request.Form["CubeSize"];
                model.AddedPatterns = "";

                if (codeGenPatternList.Count > 0) { ViewBag.disableFileName = true; } else { ViewBag.disableFileName = false; };
                if (codeGenPatternList.Count > 0) { ViewBag.disableCubeSize = true; } else { ViewBag.disableCubeSize = false; };
                return View("Create", model);
            }

            if(Char.IsDigit(fileName[0]))
            {
                ModelState.AddModelError("", "Please do not start filenames with numeric values.");
                buildSequenceList();
                model.FileName = Request.Form["FileName"];
                model.Color = Request.Form["Color"];
                model.Pattern = Request.Form["Pattern"];
                model.Speed = Request.Form["Speed"];
                model.Duration = Patterns.getDefaultDuration();
                model.CubeSize = Request.Form["CubeSize"];
                model.AddedPatterns = "";

                if (codeGenPatternList.Count > 0) { ViewBag.disableFileName = true; } else { ViewBag.disableFileName = false; };
                if (codeGenPatternList.Count > 0) { ViewBag.disableCubeSize = true; } else { ViewBag.disableCubeSize = false; };
                return View("Create", model);
            }


            string filtered = (color.StartsWith("#")) ? color.Substring(1) : color;

            //if (!string.IsNullOrWhiteSpace(speed)) { float convertSpeed = float.Parse(speed) * 1000000; speed = "" + convertSpeed; }

            //save all of the form data into a list 
            Patterns savePattern = new Patterns(pattern, Patterns.color(filtered), Patterns.duration(duration), Patterns.speed(speed), fileName, cubeSize);
            codeGenPatternList.Add(savePattern);

            //now use a for loop to build the AddedPattern list based on what has been saved in the list so the AddedPatterns box always reflects the saved list
            model.AddedPatterns = "";
            for (int i = 0; i < codeGenPatternList.Count; i++)
            {
                model.AddedPatterns += codeGenPatternList.ElementAt(i).thePattern.ToString() + "\n";
            }

            model.FileName = fileName;
            model.CubeSize = cubeSize;

            buildSequenceList();

            ViewBag.disableFileName = true;
            ViewBag.disableCubeSize = true;

            return View("Create", model);
        }

        public ActionResult RemovePattern(FormCollection form)
        {
            CreateModels model = new CreateModels();

            string fileName = Request.Form["FileName"];
            string cubeSize = Request.Form["CubeSize"];

            int lastElementIndex = codeGenPatternList.Count() - 1;

            codeGenPatternList.RemoveAt(lastElementIndex);

            model.AddedPatterns = "";
            if (codeGenPatternList.Count != 0)
            {
                for (int i = 0; i < codeGenPatternList.Count; i++)
                {
                    model.AddedPatterns += codeGenPatternList.ElementAt(i).thePattern.ToString() + "\n";
                }
            }

            model.FileName = fileName;
            model.CubeSize = cubeSize;

            buildSequenceList();
            ViewBag.disableFileName = true;
            ViewBag.disableCubeSize = true;
            return View("Create", model);
        }

        //[OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
        public ActionResult Success()
        {
            return View();
        }

        /*This uses a Viewbag to allow javascript code to access the codeGenPatternList. It allows javascript to build
        the current list stored in it*/
        public void buildSequenceList()
        {
            string[] array = new string[codeGenPatternList.Count];
            for (int i = 0; i < codeGenPatternList.Count; i++)
            {
                array[i] = codeGenPatternList.ElementAt(i).thePattern;
            }
            ViewBag.patternList = array;
        }

        /*This is for getting the list from the javascript ajax call. Then, it builds a list  I can use in C#*/
        [HttpPost]
        public EmptyResult ChangeSequence(List<int> items)
        {
            newSequenceList.Clear();
            //build the call function list in the sequence order
            for (int i = 0; i < items.Count; i++)
            {
                newSequenceList.Add(items[i]);
            }
            return new EmptyResult();
        }

        /*This will sort the newly ordered list with the old one. So, it arranges the list based on what the order of the
        sequence was. I had to convert it to a dictionary first to reorder the objects themselves that store the customization
        variables. Just changing the order of the names wouldn't work because then each pattern name would hold the incorrect
        customization variables*/
        public List<Patterns> sortLists(List<Patterns> aList)
        {
            var sorted = new Dictionary<int, Patterns>();
            for (int i = 0; i < newSequenceList.Count; i++) sorted.Add(newSequenceList[i], codeGenPatternList.ElementAt(newSequenceList[i]));
            return sorted.Values.ToList();
        }
    }
}