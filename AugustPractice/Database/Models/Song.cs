namespace MusiciansAPI.Database
{
    public class Song
    {
        public Guid Id { get; set; }
        public string SongTitle { get; set; }

        public Guid AlbumId { get; set; }
        public Album Album { get; set; }
    }
}
