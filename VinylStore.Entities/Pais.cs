using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylStore.Abstractions;

namespace VinylStore.Entities
{
    public class Pais : IEntidad, IClassMethods
    {
        public Pais()
        {
            PaisesPorArtistas = new HashSet<PaisPorArtista>();
        }
        #region properties
        public int Id { get; set; }
        [StringLength(50)]
        public string NombrePais { get; private set; }
        #endregion
        #region virtual

        public virtual ICollection<PaisPorArtista> PaisesPorArtistas { get; set; }
        #endregion
        #region getter /setter
        public void SetNombrePais(string nombrePais)
        {
            if (string.IsNullOrWhiteSpace(nombrePais))
                throw new ArgumentException("El nombre del pais no puede estar vacío.");
            NombrePais = nombrePais;
        }
        public string GetClassName()
        {
            return string.Join(": ", this.GetType().BaseType.Name, NombrePais);
        }
        #endregion

    }
}
