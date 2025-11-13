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
    public class DetallePedido:IEntidad, IClassMethods
    {
        #region properties
        public int Id { get; set; }
        [ForeignKey(nameof(Pedido))]
        public int IdPedido { get; set; }
        [ForeignKey(nameof(Vinilo))]
        public int IdVinilo { get; set; }
        public int Cantidad { get; private set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioUnitario { get; private set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal { get; private set; }
        #endregion
        #region virtual
        public virtual Pedido Pedido { get; set; }
        public virtual Vinilo Vinilo { get; set; }
        #endregion
        #region getter /setter
        public void SetCantidad(int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor que cero.");
            Cantidad = cantidad;
        }
        public void SetPrecioUnitario(decimal precioUnitario)
        {
            if (precioUnitario < 0)
                throw new ArgumentException("El precio unitario no puede ser negativo.");
            PrecioUnitario = precioUnitario;
        }
        public void SetSubtotal(decimal subtotal)
        {
            if (subtotal < 0)
                throw new ArgumentException("El subtotal no puede ser negativo.");
            Subtotal = subtotal;
        }
        public string GetClassName()
        {
            return string.Join(": ", this.GetType().BaseType.Name, Id);
        }
        #endregion
    }
}
