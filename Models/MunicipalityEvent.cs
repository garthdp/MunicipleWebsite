namespace PROG_POE.Models
{
    public class MunicipalityEvent
    {
        public MunicipalityEvent(string eventName, string eventDescription, DateTime eventDateTime, string eventCategory, string eventLocation)
        {
            EventName = eventName;
            EventDescription = eventDescription;
            EventDateTime = eventDateTime;
            EventCategory = eventCategory;
            EventLocation = eventLocation;
        }

        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventDateTime { get; set; }
        public string EventCategory { get; set; }
        public string EventLocation { get; set; }

    }
}
