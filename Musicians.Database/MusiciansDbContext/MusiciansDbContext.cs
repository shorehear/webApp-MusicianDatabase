using System;
using Microsoft.EntityFrameworkCore;

namespace Musicians.Database
{
    public class MusiciansDbContext : DbContext
    {
        public MusiciansDbContext(DbContextOptions<MusiciansDbContext> options) : base(options) { }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Musician> Musicians { get; set; }
        public DbSet<Collective> Collectives { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<User> User { get; set; }
    }
}
