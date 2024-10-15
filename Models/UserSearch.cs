namespace PROG_POE.Models
{
    public class UserSearch
    {
        public UserSearch(string? stringSearch, string? categorySearch, DateTime? dateSearch)
        {
            StringSearch = stringSearch;
            CategorySearch = categorySearch;
            DateSearch = dateSearch;
        }

        public string? StringSearch { get; set; }
        public string? CategorySearch { get; set; }
        public DateTime? DateSearch { get; set; }
    }
}
