using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PROG_POE.Models;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PROG_POE.Controllers
{
    public class EventsController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        HashSet<string> Categories = new HashSet<string>();
        EventsQueue que = new EventsQueue();
        Dictionary<string, MunicipalityEvent> Events = new Dictionary<string, MunicipalityEvent>();
        HashSet<Annoncement> Annoncements = new HashSet<Annoncement>();
        EventsPage values = new EventsPage();
        Queue<UserSearch> userSearches = new Queue<UserSearch>();
        public EventsController(IWebHostEnvironment webHostEnvironment)
        {
            // adding different event categories
            Categories.Add("Community Service");
            Categories.Add("Charity and Fundraising");
            Categories.Add("Culture and Arts");
            Categories.Add("Health and Wellness");
            Categories.Add("Enviroment");
            Categories.Add("Sports");
            Categories.Add("Meetings and Conferences");

            // adding announcements
            Annoncements.Add(new Annoncement("New Town Hall", "A new Town Hall is being built in central."));
            Annoncements.Add(new Annoncement("Loadshedding", "Loadshedding is set to begin again on the 25th of December."));
            Annoncements.Add(new Annoncement("Water", "Water will be cut off from 5pm on the 11th of November till 10am on the 12th of November."));
            Annoncements.Add(new Annoncement("Crime", "Crime is on the rise, be sure to report any suspicious activity."));
            Annoncements.Add(new Annoncement("Rubbish", "Rubbish will not be collected this comming week."));

            MunicipalityEvent event1 = new MunicipalityEvent("Beach Cleanup",
                "Clean up taking place at Kings Beach. Please wear clothes which you do not mind getting dirty.",
                DateTime.Parse("11-30-2024 14:00:00"),
                "Community Service",
                "Kings Beach, Summerstrand, Gqeberha");

            MunicipalityEvent event2 = new MunicipalityEvent("Road Cleanup",
                "We are looking for volunteers to clean up Main Road in central. Please wear clothes which you do not mind getting dirty",
                DateTime.Parse("11-11-2024 13:00:00"),
                "Community Service",
                "Main Road, Central, Gqeberha");

            MunicipalityEvent event3 = new MunicipalityEvent("Opera Show",
                "Opera show from world famous opera singer Luciano Pavarotti. Tickets cost R500. Please wear formal clothing.",
                DateTime.Parse("11-01-2024 18:00:00"),
                "Culture and Arts",
                "Mandela Bay Theatre Complex, Central, Gqeberha");

            MunicipalityEvent event4 = new MunicipalityEvent("Art Exibition",
                "Art Exibition showcasing some of Gqeberha's best artists. Tickets cost R200.",
                DateTime.Parse("11-13-2024 10:00:00"),
                "Culture and Arts",
                "GFI Art Gallery, Central, Gqeberha");

            MunicipalityEvent event5 = new MunicipalityEvent("Art Charity Auction",
                "Some pieces of art is being auctioned off for the Childrens Hope Foundation.",
                DateTime.Parse("11-24-2024 11:00:00"),
                "Charity and Fundraising",
                "GFI Art Gallery, Central, Gqeberha");

            MunicipalityEvent event6 = new MunicipalityEvent("School Musical Fundraiser",
                "ABC School is hosting a musical at the Mandela Bay Theatre Complex to fundraise money to build a hall where they will able to hosting shows in the future.",
                DateTime.Parse("11-10-2024 18:00:00"),
                "Charity and Fundraising",
                "Mandela Bay Theatre Complex, Summerstrand, Gqeberha");

            MunicipalityEvent event7 = new MunicipalityEvent("Blood Donation",
                "A blood donation will take place at Walmer Park Shopping Center. Every litre counts.",
                DateTime.Parse("11-30-2024 10:00:00"),
                "Health and Wellness",
                "Walmer Park Shopping Center, Walmer, Gqeberha");

            MunicipalityEvent event8 = new MunicipalityEvent("Old clothing and blankets donation",
                "The clothing foundation is looking for donations of old pieces of clothing and old blankets to give to the homeless.",
                DateTime.Parse("11-20-2024 10:00:00"),
                "Health and Wellness",
                "Greenacres Shopping Centre, Greenacres, Gqeberha");

            MunicipalityEvent event9 = new MunicipalityEvent("Tree Planting - Hunters Retreat",
                "Help us improve our enviroment by planting more trees.",
                DateTime.Parse("11-25-2024 10:00:00"),
                "Enviroment",
                "Baywest Mall, Hunters Retreat, Gqeberha");

            MunicipalityEvent event10 = new MunicipalityEvent("Town Meeting - Homelessness",
                "A meeting will take place at the town hall to discuss what changes can be implemented to help homeless people.",
                DateTime.Parse("11-30-2024 14:00:00"),
                "Meetings and Conferences",
                "Kings Beach, Summerstrand, Gqeberha");

            MunicipalityEvent event11 = new MunicipalityEvent("NMMU Madibaz Rugby vs Rhodes",
                "There will be a rugby match between NMU Madibaz and Rhodes. Tickets cost R100.",
                DateTime.Parse("11-26-2024 16:00:00"),
                "Sports",
                "NMU university, Summerstrand, Gqeberha");

            MunicipalityEvent event12 = new MunicipalityEvent("Eastern Province Elephants vs Sharks",
                "There will be a rugby match between Eastern Province Elephants and Sharks. Tickets cost R400.",
                DateTime.Parse("11-15-2024 16:00:00"),
                "Sports",
                "Nelson Mandela Bay Stadium, North End, Gqeberha");

            MunicipalityEvent event13 = new MunicipalityEvent("Town Meeting - Education",
                "A meeting will take place at the town hall to discuss what changes can be implemented to help provide education to all children.",
                DateTime.Parse("11-20-2024 10:00:00"),
                "Meetings and Conferences",
                "Town Hall, Central, Gqeberha");
            
            MunicipalityEvent event14 = new MunicipalityEvent("Tree Planting - Loraine",
                "Help us improve our enviroment by planting more trees.",
                DateTime.Parse("11-20-2024 10:00:00"),
                "Enviroment",
                "Loraine, Gqeberha");

            // adds events to queue
            que.enqueue(event1); 
            que.enqueue(event2);
            que.enqueue(event3);
            que.enqueue(event4);
            que.enqueue(event5);
            que.enqueue(event6);
            que.enqueue(event7);
            que.enqueue(event8);
            que.enqueue(event9);
            que.enqueue(event10);
            que.enqueue(event11);
            que.enqueue(event12);
            que.enqueue(event13);
            que.enqueue(event14);

            //taking events from queue to dictionary
            bool found = true;
            while (found)
            {
                MunicipalityEvent ev = que.dequeue();
                if (ev != null)
                {
                    Events.Add(ev.EventName.Trim().ToLower() + ":" + ev.EventDateTime, ev);
                }
                else
                {
                    found = false;
                }
            }

            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            // redirects user to sign in if not signed in
            if (HttpContext.Session.GetString("SessionUser") == "")
            {
                return RedirectToAction("SignIn", "Home");
            }
            // makes initial empty userssearches queue
            var sessionData = HttpContext.Session.GetString("UserSearches");
            if (string.IsNullOrEmpty(sessionData))
            {
                userSearches.Enqueue(new UserSearch("", "", DateTime.MinValue));
                HttpContext.Session.SetString("UserSearches", JsonConvert.SerializeObject(userSearches));
            }

            // makes initial events page
            EventsPage values = new EventsPage(Annoncements, Events, Categories);
            return View(values);
        }
        [HttpGet]
        public IActionResult Index(string searchString, string eventCategory, DateTime? eventDate)
        {
            // redirects user to sign in if not signed in
            if (HttpContext.Session.GetString("SessionUser") == "")
            {
                return RedirectToAction("SignIn", "Home");
            }
            // makes different lists from searches
            var exactEvents = from e in Events.Values
                              select e;
            var sameDayEvents = from e in Events.Values
                                where eventDate.HasValue && e.EventDateTime.Date == eventDate.Value.Date
                                select e;
            var sameCategoryEvents = from e in Events.Values
                                     where e.EventCategory == eventCategory
                                     select e;

            // makes list of events based on all search parameters. If the user didnt fill in the paramater it will just move onto the next.
            // If no paramters are filled in it will display every event.
            if (!string.IsNullOrEmpty(searchString))
            {
                exactEvents = exactEvents.Where(s => s.EventName.ToLower().Contains(searchString.Trim().ToLower()));
            }

            if (!string.IsNullOrEmpty(eventCategory))
            {
                exactEvents = exactEvents.Where(s => s.EventCategory.Equals(eventCategory));
            }

            if (eventDate.HasValue)
            {
                exactEvents = exactEvents.Where(s => s.EventDateTime.Date == eventDate.Value.Date);
            }

            //made dictionarys for the different event things such as events sharing the same category or on the same day, also the recommended events
            Dictionary<string, MunicipalityEvent> searchEvents = exactEvents.ToDictionary(e => e.EventName + ":" + e.EventDateTime);
            Dictionary<string, MunicipalityEvent> categoryEvents = sameCategoryEvents.ToDictionary(e => e.EventName + ":" + e.EventDateTime);
            Dictionary<string, MunicipalityEvent> dateEvents = sameDayEvents.ToDictionary(e => e.EventName + ":" + e.EventDateTime);
            Dictionary<string, MunicipalityEvent> recommenedEvents = makeReccomendation(searchString, eventCategory, eventDate);

            // Removes found events from recommened events so that it doesnt recommend the found event.
            if (categoryEvents.Count > 0)
            {
                foreach (var ev in categoryEvents)
                {
                    foreach (var foundEv in searchEvents)
                    {
                        if (ev.Value == foundEv.Value)
                        {
                            categoryEvents.Remove(ev.Key);
                        }
                    }
                }
            }
            // Removes found events from recommened events so that it doesnt recommend the found event.
            if (dateEvents.Count > 0)
            {
                foreach (var ev in dateEvents)
                {
                    foreach (var foundEv in searchEvents)
                    {
                        if (ev.Value == foundEv.Value)
                        {
                            dateEvents.Remove(ev.Key);
                        }
                    }
                }
            }
            // Removes found events from recommened events so that it doesnt recommend the found event.
            if (recommenedEvents.Count > 0)
            {
                foreach (var ev in recommenedEvents)
                {
                    foreach (var foundEv in searchEvents)
                    {
                        if (ev.Value == foundEv.Value)
                        {
                            recommenedEvents.Remove(ev.Key);
                        }
                    }
                }
            }

            // makes object to be returned to page
            var model = new EventsPage
            {
                annoncements = Annoncements,
                municipalityEvents = searchEvents,
                categorySearchEvents = categoryEvents,
                dateSearchEvents = dateEvents,
                recommendedEvents = recommenedEvents,
                categories = Categories
            };

            return View(model);
        }
        public Dictionary<string, MunicipalityEvent> makeReccomendation(string searchString, string category, DateTime? date)
        {
            // gets users previous searchers
            var sessionData = HttpContext.Session.GetString("UserSearches");

            // initializes usersearches if its empty
            if (string.IsNullOrEmpty(sessionData))
            {
                userSearches = new Queue<UserSearch>();
            }
            // retrieves searches if it is not empty
            else
            {
                userSearches = JsonConvert.DeserializeObject<Queue<UserSearch>>(sessionData);
            }

            // makes user search object
            UserSearch searches = new UserSearch(searchString, category, date);

            // adds it to queue
            userSearches.Enqueue(searches);

            // makes hashset of recommended events to prevent adding same event more than twice
            HashSet<MunicipalityEvent> recommendedHashsetEvents = new HashSet<MunicipalityEvent>();

            // makes initial dictionary which will store recommended events
            Dictionary<string, MunicipalityEvent> recommendedEvents = new Dictionary<string, MunicipalityEvent>();

            // queue only stores five searches before it dequeues the oldest search
            // this is to prevent recommending something the user is no longer interested in
            if (userSearches.Count > 5)
            {
                userSearches.Dequeue();
            }
            // addeds recommended events to recommended events hashset based on previous searches
            foreach (var search in userSearches)
            {
                foreach (var ev in Events.Values)
                {
                    if (!string.IsNullOrEmpty(search.StringSearch))
                    {
                        List<string> keywords = search.StringSearch.Trim().ToLower().Split(" ").ToList();
                        for (int i = 0; i < keywords.Count; i++)
                        {
                            if (ev.EventName.ToLower().Contains(keywords[i]))
                            {
                                recommendedHashsetEvents.Add(ev);
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(search.CategorySearch))
                    {
                        if (ev.EventCategory == search.CategorySearch)
                        {
                            recommendedHashsetEvents.Add(ev);
                        }
                    }
                    if (search.DateSearch.HasValue)
                    {
                        if (ev.EventDateTime.Date == search.DateSearch.Value.Date)
                        {
                            recommendedHashsetEvents.Add(ev);
                        }
                    }
                }
            }

            // adds to dictionary from hashset
            foreach (var e in recommendedHashsetEvents)
            {
                recommendedEvents.Add(e.EventName.Trim().ToLower() + ":" + e.EventDateTime.Date, e);
            }

            // saves queue to session again
            string saveSearches = JsonConvert.SerializeObject(userSearches);
            HttpContext.Session.SetString("UserSearches", saveSearches);
            // returns recommended events
            return recommendedEvents;
        }
    }
}
