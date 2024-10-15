namespace PROG_POE.Models
{
    public class EventsPage
    {
        public EventsPage(HashSet<Annoncement> annoncements, Dictionary<string, MunicipalityEvent> municipalityEvents, HashSet<string> categories)
        {
            this.annoncements = annoncements;
            this.municipalityEvents = municipalityEvents;
            this.categories = categories;
        }
        public EventsPage(HashSet<Annoncement> annoncements, Dictionary<string, MunicipalityEvent> municipalityEvents, Dictionary<string, MunicipalityEvent> categorySearchEvents, Dictionary<string, MunicipalityEvent> recommendedEvents, Dictionary<string, MunicipalityEvent> dateSearchEvents, HashSet<string> categories)
        {
            this.annoncements = annoncements;
            this.municipalityEvents = municipalityEvents;
            this.categories = categories;
            this.dateSearchEvents = dateSearchEvents;
            this.categorySearchEvents = categorySearchEvents;
            this.recommendedEvents = recommendedEvents;
        }
        public EventsPage()
        {
        }

        public HashSet<Annoncement> annoncements { get; set; }
        public Dictionary<string, MunicipalityEvent> municipalityEvents { get; set; }
        public HashSet<string> categories { get; set; }
        public Dictionary<string, MunicipalityEvent> categorySearchEvents { get; set; }
        public Dictionary<string, MunicipalityEvent> dateSearchEvents { get; set; }
        public Dictionary<string, MunicipalityEvent> recommendedEvents { get; set; }

    }
}
