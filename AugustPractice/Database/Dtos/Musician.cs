namespace MusiciansAPI.Database
{
    public class MusicianDto
    {
        public Guid Id { get; set; }
        public string MusicianName { get; set; }
        public DateTime MusicianBirthDate { get; set; }
        public DateTime? MusicianDeathDate { get; set; }

        public CountryDto Country { get; set; }
        public CollectiveDto? Collective { get; set; }
        public IEnumerable<AlbumDto>? Albums { get; set; }
    }
}
