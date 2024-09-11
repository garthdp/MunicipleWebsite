namespace PROG_POE.Models
{
    public class Report
    {
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public Report(string location, string category, string description, string imagePath)
        {
            Location = location;
            Category = category;
            Description = description;
            ImagePath = imagePath;
        }
        public Report()
        {
        }
    }
}
