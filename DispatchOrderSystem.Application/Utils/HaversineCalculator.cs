namespace DispatchOrderSystem.Application.Utils
{
    public class HaversineCalculator
    {
        public static double CalculateDistanceKm(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371; // Radius of the Earth in kilometers
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);

            var a = Math.Pow(Math.Sin(dLat / 2), 2) +
                Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                Math.Pow(Math.Sin(dLon / 2), 2);

            var c = 2 * Math.Asin(Math.Sqrt(a));
            return R * c; // Distance in kilometers
        }

        private static double ToRadians(double angle) => angle * Math.PI / 180;
    }
}
