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
    public class Pedido:IEntidad, IClassMethods
    {
        public Pedido()
        {
            DetallesPedidos= new HashSet<DetallePedido>();
            FechaPedido = DateTime.UtcNow;
            Estado = "Pendiente"; // Estado inicial
        }
        #region properties
        public int Id { get; set; }
        [ForeignKey(nameof(User))]
        public Guid IdUser { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaPedido { get; private set; }
        [MaxLength(50)]
        public string Estado { get; private set; } // Pendiente, Procesando, Enviado, Completado, Cancelado
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; private set; }
        [MaxLength(100)]
        public string MetodoDePago { get; private set; }
        #endregion
        #region virtual
        public virtual User User { get; set; }
        public virtual ICollection<DetallePedido> DetallesPedidos { get; set; }
        #endregion
        #region getter /setter
        public void SetFechaPedido(DateTime fechaPedido)
        {
            if (fechaPedido > DateTime.Now)
                throw new ArgumentException("La fecha del pedido no puede ser futura.");
            FechaPedido = fechaPedido;
        }
        public void SetEstado(string estado)
        {
            var estadosValidos = new[] { "Pendiente", "Procesando", "Enviado", "Completado", "Cancelado" };
            if (!estadosValidos.Contains(estado))
                throw new ArgumentException($"Estado inválido. Debe ser uno de: {string.Join(", ", estadosValidos)}");
            Estado = estado;
        }
        public void SetTotal(decimal total)
        {
            if (total < 0)
                throw new ArgumentException("El total del pedido no puede ser negativo.");
            Total = total;
        }
        public void SetMetodoDePago(string metodoDePago)
        {
            if (string.IsNullOrWhiteSpace(metodoDePago))
                throw new ArgumentException("El método de pago no puede estar vacío.");
            MetodoDePago = metodoDePago;
        }

        public string GetClassName()
        {
            return string.Join(": ", this.GetType().BaseType.Name, Id);
        }
        #endregion

    }
}
