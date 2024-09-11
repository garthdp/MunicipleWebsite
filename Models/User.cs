namespace PROG_POE.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public User(string username, string password, int level, int experience)
        {
            Username = username;
            Password = password;
            Level = level;
            Experience = experience;
        }
        public User()
        {
        }
        public void addExperience(int xp)
        {
            Experience += xp;
            if(Experience >= 100)
            {
                Experience -= 100;
                Level += 1;
            }
        }
    }
}
