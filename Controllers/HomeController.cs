using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PROG_POE.Models;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Security.Policy;

namespace PROG_POE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // used for uploading files
        private readonly IWebHostEnvironment _webHost;
        List<Report> reports = new List<Report>();
        List<User> users = new List<User>();
        private string reportString = "";
        private string userString = "";
        private User currentUser;
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHost)
        {
            _logger = logger;
            _webHost = webHost;
        }

        //shows home page
        public IActionResult Index()
        {
            // redirects user to sign in if not signed in
            if(HttpContext.Session.GetString("SessionUser") == "")
            {
                return RedirectToAction("SignIn");
            }
            return View();
        }

        // loads user profile
        public IActionResult Profile()
        {
            // redirects user to sign in if not signed in
            if (HttpContext.Session.GetString("SessionUser") == "")
            {
                return RedirectToAction("SignIn");
            }

            User user = new User();
            List<User> users = new List<User>();

            //gets saved users from session
            users = JsonConvert.DeserializeObject<List<User>>(HttpContext.Session.GetString("Users"));

            //finds user from list and gets users details
            foreach (User usera in users)
            {
                if (usera.Username == HttpContext.Session.GetString("SessionUser"))
                {
                    user = usera;
                    break;
                }
            }

            // assigns user details to page
            ViewBag.Username = user.Username;
            ViewBag.Level = user.Level;
            ViewBag.Experience = user.Experience;
            return View();
        }

        //signs user out and redirects to signin
        public ActionResult SignOut()
        {
            HttpContext.Session.SetString("SessionUser", "");
            return RedirectToAction("SignIn", "Home");
        }
        public IActionResult Leaderboard()
        {
            // redirects user to sign in if not signed in
            if (HttpContext.Session.GetString("SessionUser") == "")
            {
                return RedirectToAction("SignIn");
            }
            List<User> users = new List<User>();

            //gets list of users from session
            users = JsonConvert.DeserializeObject<List<User>>(HttpContext.Session.GetString("Users"));

            //sorts users by highest level to lowest level
            var sortedUsersByLevel = users.OrderByDescending(x => x.Level);
            ViewBag.Users = sortedUsersByLevel;
            return View();
        }

        // shows list of reports
        public IActionResult ReportList()
        {
            // redirects user to sign in if not signed in
            if (HttpContext.Session.GetString("SessionUser") == "")
            {
                return RedirectToAction("SignIn");
            }

            // gets list of reports from session
            reports = JsonConvert.DeserializeObject<List<Report>>(HttpContext.Session.GetString("Reports"));
            ViewBag.Reports = reports;
            return View();
        }
        public IActionResult SignIn()
        {
            /* 
             Code Attribution
             Title: How to save List or Object in Session in ASP MVC .NET 7 | Session in .NET 7
             Used for: to save lists to session
             Made by: Noor Codelogics
             Link: https://www.youtube.com/watch?v=6bPeFO10GN4
             */

            reports.Add(new Report("Location Test", "Roads", "Lots of potholes", "Example of pothole"));

            //makes report list for session on sign in 
            string reportsString = JsonConvert.SerializeObject(reports);
            HttpContext.Session.SetString("Reports", reportsString);

            //makes user list for session on sign in 
            users.Add(new User("Garth", "password", 5, 0));
            users.Add(new User("Ryan", "password", 3, 50));
            users.Add(new User("Seth", "password", 2, 0));
            users.Add(new User("Nate", "password", 7, 50));
            string usersString = JsonConvert.SerializeObject(users);
            HttpContext.Session.SetString("Users", usersString);

            return View();
        }

        //signs in user
        [HttpPost]
        public IActionResult SignIn(User user)
        {
            // gets list of users
            List<User> users = new List<User>();
            users = JsonConvert.DeserializeObject<List<User>>(HttpContext.Session.GetString("Users"));
            bool found = false;

            // checks if inputted user info is correct or not
            foreach(User usera in users)
            {
                if(usera.Username == user.Username && usera.Password == user.Password)
                {
                    HttpContext.Session.SetString("SessionUser", usera.Username);
                    found = true;
                }
            }
            if (found)
            {
                return RedirectToAction("Index");
            }
            else
            {
                /* 
                    Code Attribution
                    Title: Display Invalid Login Details message using MVC code
                    Used for: to display error message when signing in with incorrect information
                    Made by: Rahul Sharma
                    Stackoverflow post: https://stackoverflow.com/questions/55391046/display-invalid-login-details-message-using-mvc-code
                    Answer User's Profile Link: https://stackoverflow.com/users/1807452/rahul-sharma
                */
                ModelState.AddModelError(string.Empty, "Invalid Credentials Supplied");
                return View();
            }
        }

        // loads create report page
        public IActionResult CreateReport()
        {
            //makes report list for session on sign in 
            if (HttpContext.Session.GetString("SessionUser") == "")
            {
                return RedirectToAction("SignIn");
            }

            //fills categorys selectlist
            List<SelectListItem> categories = new List<SelectListItem>();
            SelectListItem category1 = new SelectListItem();
            category1.Text = "Sanitation";
            category1.Value = "Sanitation";
            categories.Add(category1);
            SelectListItem category2 = new SelectListItem();
            category2.Text = "Roads";
            category2.Value = "Roads";
            categories.Add(category2);
            SelectListItem category3 = new SelectListItem();
            category3.Text = "Utilities";
            category3.Value = "Utilities";
            categories.Add(category3);
            SelectListItem category4 = new SelectListItem();
            category4.Text = "Electricity";
            category4.Value = "Electricity";
            categories.Add(category4);
            ViewBag.Categories = categories;
            return View();
        }

        // creates report and saves it to list
        [HttpPost]
        public async Task<IActionResult> CreateReport(Report report, IFormFile SingleFile)
        {
            //makes report list for session on sign in 
            if (HttpContext.Session.GetString("SessionUser") == "")
            {
                return RedirectToAction("SignIn");
            }

            /* 
             Code Attribution
             Title: How to upload file in Asp.Net Core MVC | C# | IAmUmair
             Used for: to save files 
             Made by: IAmUmair
             Link: https://www.youtube.com/watch?v=J2aoApd3mNA
             */

            // saves data to uploads folder
            string uploadsFolder = Path.Combine(_webHost.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string fileName = Path.GetFileName(SingleFile.FileName);
            string fileSavePath = Path.Combine(uploadsFolder, fileName);

            using (FileStream stream = new FileStream(fileSavePath, FileMode.Create))
            {
                report.ImagePath = SingleFile.FileName;
                await SingleFile.CopyToAsync(stream);
            }

            // gets reports list
            List<Report> reports = new List<Report>();
            reports = JsonConvert.DeserializeObject<List<Report>>(HttpContext.Session.GetString("Reports"));

            // adds reports to list
            reports.Add(report);

            // saves list to session again with new report
            string reportsString = JsonConvert.SerializeObject(reports);
            HttpContext.Session.SetString("Reports", reportsString);

            // gets lists of users
            users = JsonConvert.DeserializeObject<List<User>>(HttpContext.Session.GetString("Users"));
            currentUser = users.Where(x => x.Username == HttpContext.Session.GetString("SessionUser")).First();

            // adds experience to users account on report completion
            currentUser.addExperience(50);

            // finds user
            int index = users.FindIndex(x => x.Username == HttpContext.Session.GetString("SessionUser"));

            //removes out of date user info
            users.RemoveAt(index);

            // adds updated info to user
            users.Add(currentUser);

            // saves updated list to session
            string usersString = JsonConvert.SerializeObject(users);
            HttpContext.Session.SetString("Users", usersString);

            return RedirectToAction("ReportList");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
