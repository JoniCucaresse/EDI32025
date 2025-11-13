using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylStore.Abstractions;

namespace VinylStore.Entities
{
    public class Vinilo : IEntidad, IClassMethods
    {
        public Vinilo()
        {
            ArtistasPorVinilos = new HashSet<ArtistaPorVinilo>();
            GenerosPorVinilos = new HashSet<GeneroPorVinilo>();
        }
        #region properties
        public int Id { get; set; }
        [StringLength(50)]
        public string Titulo { get; private set; }
        public int? AnioLanzamiento { get; private set; }
        [StringLength(100)]
        public string Descripcion { get; private set; }
        [ForeignKey(nameof(Discografica))]
        public int IdDiscografica { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; private set; }
        public int Stock { get; private set; }
        #endregion
        #region virtual
        public virtual Discografica Discografica { get; set; }
        public virtual ICollection<ArtistaPorVinilo> ArtistasPorVinilos { get; set; }
        public virtual ICollection<GeneroPorVinilo> GenerosPorVinilos { get; set; }
        #endregion
        #region getter /setter
        public void SetTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("El título del vinilo no puede estar vacío.");
            Titulo = titulo;
        }
        public void SetAnioLanzamiento(int? anioLanzamiento)
        {
            if (anioLanzamiento.HasValue && (anioLanzamiento < 1900 || anioLanzamiento > DateTime.Now.Year))
                throw new ArgumentException("El año de lanzamiento no es válido.");
            AnioLanzamiento = anioLanzamiento;
        }
        public void SetDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
                throw new ArgumentException("La descripción no puede estar vacía.");
            Descripcion = descripcion;
        }
        public void SetPrecio(decimal precio)
        {
            if (precio < 0)
                throw new ArgumentException("El precio no puede ser negativo.");
            Precio = precio;
        }
        public void SetStock(int stock)
        {
            if (stock < 0)
                throw new ArgumentException("El stock no puede ser negativo.");
            Stock = stock;

        }
        public string GetClassName()
        {
            return string.Join(": ", this.GetType().BaseType.Name, Titulo);
        }

        #endregion
    }
}
