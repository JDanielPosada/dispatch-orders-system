namespace DispatchOrderSystem.Domain.Aggregates.ValueObjects
{
    public class Coordinates
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public Coordinates(double latitude, double longitude)
        {
            if (latitude < -90 || latitude > 90)
                throw new ArgumentOutOfRangeException(nameof(latitude), "Latitude must be between -90 and 90 degrees.");
            if (longitude < -180 || longitude > 180)
                throw new ArgumentOutOfRangeException(nameof(longitude), "Longitude must be between -180 and 180 degrees.");

            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
