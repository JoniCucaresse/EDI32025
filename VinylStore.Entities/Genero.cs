using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylStore.Abstractions;

namespace VinylStore.Entities
{
    public class Genero : IEntidad, IClassMethods
    {
        public Genero()
        {
            GenerosPorVinilos = new HashSet<GeneroPorVinilo>();
        }
        #region properties
        public int Id { get; set; }
        [StringLength(50)]
        public string Nombre { get; private set; }
        #endregion
        #region virtual
        public virtual ICollection<GeneroPorVinilo> GenerosPorVinilos { get; set; }
        #endregion
        #region getter /setter
        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del genero no puede estar vacío.");
            Nombre = nombre;
        }
        public string GetClassName()
        {
            return string.Join(": ", this.GetType().BaseType.Name, Nombre);
        }
        #endregion
    }
}
