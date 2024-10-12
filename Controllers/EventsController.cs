using Microsoft.AspNetCore.Mvc;
using PROG_POE.Models;
using System.Diagnostics;

namespace PROG_POE.Controllers
{
    public class EventsController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        HashSet<string> Categories = new HashSet<string>();
        EventsQueue que = new EventsQueue();
        Dictionary<string, MunicipalityEvent> Events = new Dictionary<string, MunicipalityEvent>();
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

            MunicipalityEvent event9 = new MunicipalityEvent("Tree Planting",
                "Help us improve our enviroment by planting more trees.",
                DateTime.Parse("11-25-2024 10:00:00"),
                "Enviroment",
                "Baywest Mall, Hunters Retreat, Gqeberha");

            MunicipalityEvent event10 = new MunicipalityEvent("Town Meeting",
                "A meeting will take place at the town hall to discuss what changes can be implemented to help homeless people.",
                DateTime.Parse("11-30-2024 14:00:00"),
                "Community Service",
                "Kings Beach, Summerstrand, Gqeberha");

            MunicipalityEvent event11 = new MunicipalityEvent("NMMU Madibaz Rugby vs Rhodes",
                "There will be a rugby match between NMU Madibaz and Rhodes. Tickets cost R100.",
                DateTime.Parse("11-26-2024 16:00:00"),
                "Sport",
                "NMU university, Summerstrand, Gqeberha");

            MunicipalityEvent event12 = new MunicipalityEvent("Eastern Province Elephants vs Sharks",
                "There will be a rugby match between Eastern Province Elephants and Sharks. Tickets cost R400.",
                DateTime.Parse("11-15-2024 16:00:00"),
                "Sport",
                "Nelson Mandela Bay Stadium, North End, Gqeberha");

            MunicipalityEvent event13 = new MunicipalityEvent("Town Meeting",
                "A meeting will take place at the town hall to discuss what changes can be implemented to help provide education to all children.",
                DateTime.Parse("11-20-2023 10:00:00"),
                "Meetings and Conferences",
                "Town Hall, Central, Gqeberha");
            
            MunicipalityEvent event14 = new MunicipalityEvent("Tree Planting",
                "Help us improve our enviroment by planting more trees.",
                DateTime.Parse("11-20-2023 10:00:00"),
                "Enviroment",
                "Loraine, Gqeberha");

            // add events to dictionary
            Events.Add(event1.EventName + ":" + event1.EventDateTime, event1);
            Events.Add(event2.EventName + ":" + event2.EventDateTime, event2);
            Events.Add(event3.EventName + ":" + event3.EventDateTime, event3);
            Events.Add(event4.EventName + ":" + event4.EventDateTime, event4);
            Events.Add(event5.EventName + ":" + event5.EventDateTime, event5);
            Events.Add(event6.EventName + ":" + event6.EventDateTime, event6);
            Events.Add(event7.EventName + ":" + event7.EventDateTime, event7);
            Events.Add(event8.EventName + ":" + event8.EventDateTime, event8);
            Events.Add(event9.EventName + ":" + event9.EventDateTime, event9);
            Events.Add(event10.EventName + ":" + event10.EventDateTime, event10);
            Events.Add(event11.EventName + ":" + event11.EventDateTime, event11);
            Events.Add(event12.EventName + ":" + event12.EventDateTime, event12);
            Events.Add(event13.EventName + ":" + event13.EventDateTime, event13);
            Events.Add(event14.EventName + ":" + event14.EventDateTime, event14);

            foreach(KeyValuePair<string, MunicipalityEvent> ev in Events)
            {
                que.enqueue(ev.Value);
            }

            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            que.peek();
            var events = Events.Values.ToList();
            return View(events);
        }
    }
}
