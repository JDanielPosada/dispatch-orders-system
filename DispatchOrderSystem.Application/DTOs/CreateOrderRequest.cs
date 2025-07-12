namespace DispatchOrderSystem.Application.DTOs
{
    /// <summary>
    /// Representa la solicitud para crear una nueva orden de despacho.
    /// </summary>
    public class CreateOrderRequest
    {
        /// <summary>
        /// Identificador único del cliente que realiza la orden.
        /// </summary>
        public Guid ClientId { get; set; }

        /// <summary>
        /// Identificador único del producto solicitado.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Cantidad del producto solicitada.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Latitud del punto de origen del despacho.
        /// </summary>
        public double OriginLatitude { get; set; }

        /// <summary>
        /// Longitud del punto de origen del despacho.
        /// </summary>
        public double OriginLongitude { get; set; }

        /// <summary>
        /// Latitud del punto de destino del despacho.
        /// </summary>
        public double DestinationLatitude { get; set; }

        /// <summary>
        /// Longitud del punto de destino del despacho.
        /// </summary>
        public double DestinationLongitude { get; set; }
    }
}
