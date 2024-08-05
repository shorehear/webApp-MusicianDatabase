namespace MusiciansAPI.Database
{
    public class SongDto
    {
        public Guid Id { get; set; }
        public string SongTitle { get; set; }

        public Guid AlbumId { get; set; }
        public AlbumDto Album { get; set; }
    }
}
