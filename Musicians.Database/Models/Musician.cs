namespace Musicians.Database
{
    public class Musician
    {
        public Guid Id { get; set; }
        public string MusicianName { get; set; }
        public DateTime MusicianBirthDate {  get; set; }
        public DateTime? MusicianDeathDate { get; set; }

        public Guid CountryId { get; set; }
        public Country Country { get; set; }
        public Guid? CollectiveId {  get; set; }
        public Collective Collective { get; set; }
        public ICollection<Album> Albums { get; set; } = new List<Album>();
    }
}
