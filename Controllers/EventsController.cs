using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        HashSet<Annoncement> Annoncements = new HashSet<Annoncement>();
        EventsPage values = new EventsPage();
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

            bool found = true;
            while (found)
            {
                MunicipalityEvent ev = que.dequeue();
                if (ev != null)
                {
                    Events.Add(ev.EventName + ":" + ev.EventDateTime, ev);
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
            que.peek();

            EventsPage values = new EventsPage(Annoncements, Events);
            return View(values);
        }
    }
}
