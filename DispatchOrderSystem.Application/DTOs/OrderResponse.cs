namespace DispatchOrderSystem.Application.DTOs
{
    /// <summary>
    /// Resumen de una orden de despacho.
    /// </summary>
    public class OrderResponse
    {
        /// <summary>
        /// Identificador único de la orden.
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// Nombre del cliente que realizó la orden.
        /// </summary>
        public string ClientName { get; set; } = default!;

        /// <summary>
        /// Nombre del producto solicitado.
        /// </summary>
        public string ProductName { get; set; } = default!;

        /// <summary>
        /// Distancia en kilómetros entre el origen y destino.
        /// </summary>
        public double DistanceKm { get; set; }

        /// <summary>
        /// Costo del despacho de la orden.
        /// </summary>
        public decimal Cost { get; set; }
    }
}
