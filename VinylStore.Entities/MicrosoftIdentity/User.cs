using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylStore.Entities.MicrosoftIdentity
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            Pedidos = new HashSet<Pedido>();
            Reseñas = new HashSet<Reseña>();
        }
        #region properties
        [Required(ErrorMessage = "{0} Required")]
        [StringLength(100)]
        [PersonalData]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "{0} Required")]
        [StringLength(100)]
        [PersonalData]
        public string Apellidos { get; set; }
        [DataType(DataType.Date)]
        public DateTime? FechaNacimiento { get; set; }
        [StringLength(100)]
        public string DireccionEnvio { get; set; }



        #endregion
        #region virtual
        public virtual ICollection<Pedido> Pedidos { get; set; }
        public virtual ICollection<Reseña> Reseñas { get; set; }
        #endregion

    }
}
