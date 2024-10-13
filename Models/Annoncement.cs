namespace PROG_POE.Models
{
    public class Annoncement
    {
        public Annoncement(string annoncementTitle, string annoncementMessage)
        {
            AnnoncementTitle = annoncementTitle;
            AnnoncementMessage = annoncementMessage;
        }

        public string AnnoncementTitle { get; set; }
        public string AnnoncementMessage { get; set; }
    }
}
