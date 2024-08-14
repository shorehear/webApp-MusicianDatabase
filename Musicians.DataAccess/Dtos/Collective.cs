using Musicians.Database;

namespace Musicians.DataAccess
{
    public class CollectiveDto
    {
        public Guid Id { get; set; }
        public string CollectiveName { get; set; }
        public Genre CollectiveGenre { get; set; }

        public IEnumerable<MusicianDto>? CollectiveMembers { get; set; } 
        public IEnumerable<AlbumDto>? Albums { get; set; }  
    }
}
