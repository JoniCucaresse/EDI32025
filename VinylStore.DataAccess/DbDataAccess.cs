using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylStore.Entities;
using VinylStore.Entities.MicrosoftIdentity;

namespace VinylStore.DataAccess
{
    public class DbDataAccess : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public virtual DbSet<PaisPorArtista> PaisesPorArtistas { get; set; }
        public virtual DbSet<Pais> Paises { get; set; }
        public virtual DbSet<Vinilo> Vinilos { get; set; }
        public virtual DbSet<Artista> Artistas { get; set; }
        public virtual DbSet<Discografica> Discograficas { get; set; }
        public virtual DbSet<Genero> Generos { get; set; }
        public virtual DbSet<ArtistaPorVinilo> ArtistasPorVinilos { get; set; }
        public virtual DbSet<GeneroPorVinilo> GenerosPorVinilos { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Pedido> Pedidos { get; set; }
        public virtual DbSet<DetallePedido> DetallesPedidos { get; set; }
        public virtual DbSet<Reseña> Reseñas { get; set; }
        public DbDataAccess(DbContextOptions<DbDataAccess> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.LogTo(Console.WriteLine).EnableDetailedErrors();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
