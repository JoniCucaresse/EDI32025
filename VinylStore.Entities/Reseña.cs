using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylStore.Abstractions;
using VinylStore.Entities.MicrosoftIdentity;

namespace VinylStore.Entities
{
    public class Reseña: IEntidad, IClassMethods
    {
        #region properties
        public int Id { get; set; }
        [ForeignKey(nameof(Vinilo))]
        public int IdVinilo { get; set; }
        [ForeignKey(nameof(User))]
        public Guid IdUser { get; set; }
        [Range(1, 5)]
        public int Rating { get; private set; }
        [MaxLength(1000)]
        public string Comentario { get; private set; }
        [DataType(DataType.Date)]
        public DateTime FechaReseña { get; private set; } = DateTime.UtcNow;
        #endregion
        #region virtual
        public virtual Vinilo Vinilo { get; set; }
        public virtual User User { get; set; }
        #endregion
        #region getter /setter
        public void SetRating(int rating)
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentException("El rating debe estar entre 1 y 5.");
            Rating = rating;
        }
        public void SetComentario(string comentario)
        {
            if (string.IsNullOrWhiteSpace(comentario))
                throw new ArgumentException("El comentario no puede estar vacío.");
            Comentario = comentario;
        }
        public void SetFechaReseña(DateTime fechaReseña)
        {
            if (fechaReseña > DateTime.Now)
                throw new ArgumentException("La fecha de la reseña no puede ser futura.");
            FechaReseña = fechaReseña;
        }
        public string GetClassName()
        {
            return string.Join(": ", this.GetType().BaseType.Name, Id);
        }

        #endregion
    }
}
