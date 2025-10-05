using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylStore.Entities;

namespace VinylStore.DataAccess
{
    public class DbDataAccess : IdentityDbContext
    {
        public virtual DbSet<Vinilo> Vinilos { get; set; }
        public virtual DbSet<Artista> Artistas { get; set; }
        public virtual DbSet<Discografica> Discograficas { get; set; }
        public virtual DbSet<Genero> Generos { get; set; }
        public virtual DbSet<ArtistaPorVinilo> ArtistasPorVinilos { get; set; }
        public virtual DbSet<GeneroPorVinilo> GenerosPorVinilos { get; set; }
        public DbDataAccess(DbContextOptions<DbDataAccess> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.LogTo(Console.WriteLine).EnableDetailedErrors();
    }
}
