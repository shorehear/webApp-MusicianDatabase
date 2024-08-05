namespace MusiciansAPI.Database
{
    public class Collective
    {
        public Guid Id { get; set; }
        public string CollectiveName { get; set; }
        public Genre CollectiveGenre { get; set; }
        public ICollection<Musician> CollectiveMembers { get; set; }
        public ICollection<Album> Albums { get; set; }
    }
}
