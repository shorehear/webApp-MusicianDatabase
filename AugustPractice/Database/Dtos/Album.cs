namespace MusiciansAPI.Database
{
    public class AlbumDto
    {
        public Guid Id { get; set; }
        public string AlbumTitle { get; set; }
        public int NumberOfTracks { get; set; }
        public int ReleaseYear { get; set; }

        public CollectiveDto? Collective { get; set; }
        public MusicianDto? Musician { get; set; }
    }
}
