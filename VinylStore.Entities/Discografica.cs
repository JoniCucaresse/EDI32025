using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylStore.Abstractions;

namespace VinylStore.Entities
{
    public class Discografica : IEntidad, IClassMethods
    {
        public Discografica()
        {
            Vinilos = new HashSet<Vinilo>();
        }
        #region properties
        public int Id { get; set; }
        public string Nombre { get; private set; }
        #endregion
        #region virtual
        public virtual ICollection<Vinilo> Vinilos { get; set; }


        #endregion
        #region getter /setter
        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre de la discografica no puede estar vacío.");
            Nombre = nombre;
        }
        public string GetName()
        {
            return string.Join(": ", this.GetName(), Nombre);
        }
        public string GetClassName()
        {
            return string.Join(": ", this.GetType().BaseType.Name, Nombre);
        }


        #endregion
    }
}
