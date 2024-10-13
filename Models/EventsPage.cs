namespace PROG_POE.Models
{
    public class EventsPage
    {
        public EventsPage(HashSet<Annoncement> annoncements, Dictionary<string, MunicipalityEvent> municipalityEvents)
        {
            this.annoncements = annoncements;
            this.municipalityEvents = municipalityEvents;
        }
        public EventsPage()
        {
        }

        public HashSet<Annoncement> annoncements { get; set; }
        public Dictionary<string, MunicipalityEvent> municipalityEvents { get; set; }
    }
}
