using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace MusiciansAPI.Database
{
    public class MusiciansDbContext : DbContext
    {
        public MusiciansDbContext (DbContextOptions<MusiciansDbContext> options)
            : base(options) { }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Musician> Musicians { get; set; }
        public DbSet<Collective> Collectives { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }        
    }
}
