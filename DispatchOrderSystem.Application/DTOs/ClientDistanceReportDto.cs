namespace DispatchOrderSystem.Application.DTOs
{
    /// <summary>
    /// Reporte de número de órdenes agrupadas por rangos de distancia para un cliente.
    /// </summary>
    public class ClientDistanceReportDto
    {
        /// <summary>
        /// Nombre del cliente.
        /// </summary>
        public string ClientName { get; set; } = string.Empty;

        /// <summary>
        /// Número de órdenes con distancia entre 1 y 50 km.
        /// </summary>
        public int Orders_1_50_Km { get; set; }

        /// <summary>
        /// Número de órdenes con distancia entre 51 y 200 km.
        /// </summary>
        public int Orders_51_200_Km { get; set; }

        /// <summary>
        /// Número de órdenes con distancia entre 201 y 500 km.
        /// </summary>
        public int Orders_201_500_Km { get; set; }

        /// <summary>
        /// Número de órdenes con distancia entre 501 y 1000 km.
        /// </summary>
        public int Orders_501_1000_Km { get; set; }

        /// <summary>
        /// Número total de órdenes sumando todos los rangos de distancia.
        /// </summary>
        public int TotalOrders => Orders_1_50_Km + Orders_51_200_Km + Orders_201_500_Km + Orders_501_1000_Km;
    }
}
