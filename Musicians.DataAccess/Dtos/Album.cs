using Musicians.Database;

namespace Musicians.DataAccess
{
    public class AlbumDto
    {
        public Guid Id { get; set; }
        public string AlbumTitle { get; set; }

        public CollectiveDto? Collective { get; set; }
        public MusicianDto? Musician { get; set; }
    }
}
