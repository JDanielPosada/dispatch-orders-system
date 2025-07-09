namespace DispatchOrderSystem.Domain.Enums
{
    public enum DistanceRange
    {
        Invalid = 0,
        Short = 1, // 0 to 50 km
        Medium = 2, // 51 to 200 km
        Long = 3, // 201 to 500 km
        Extended = 4, // 501 to 1000 km
    }
}
