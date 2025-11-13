using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylStore.Abstractions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VinylStore.Entities
{
    public class Artista : IEntidad
    {
        public Artista()
        {
            ArtistasPorVinilos = new HashSet<ArtistaPorVinilo>();
        }
        #region properties
        public int Id { get; set; }
        [StringLength(50)]
        public string Nombre { get; private set; }
        [ForeignKey(nameof(Pais))]
        public int IdPais { get; set; }
        public string Biografia { get; private set; }
        #endregion
        #region virtual
        public virtual Pais Pais { get; set; }
        public virtual ICollection<ArtistaPorVinilo> ArtistasPorVinilos { get; set; }
        #endregion
        #region getter /setter
        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del artista no puede estar vacío.");
            Nombre = nombre;
        }
        public void SetBiografia(string biografia)
        {
            if (string.IsNullOrWhiteSpace(biografia))
                throw new ArgumentException("La biografia no puede estar vacia.");
            Biografia = biografia;
        }
        #endregion
    }
}
