namespace MusiciansAPI.Database
{
    public class Album
    {
        public Guid Id { get; set; }
        public string AlbumTitle { get; set; }
        public int NumberOfTracks { get; set; }
        public int ReleaseYear { get; set; }

        public Guid? CollectiveId { get; set; }
        public Collective Collective { get; set; }

        public Guid? MusicianId { get; set; }
        public Musician Musician { get; set; }
    }
}
