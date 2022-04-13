using System;

namespace Easee.Cos
{
    public class Position : IComparable<Position>
    {
        public Position(double latitude, double longitude, double? altitude = null, double? dop = null)
        {
            Latitude = latitude;
            Longitude = longitude;
            Altitude = altitude;
            DOP = dop;
        }

        public double Latitude { get; }
        public double Longitude { get; }
        public double? Altitude { get; }
        public double? DOP { get; }

        public int CompareTo(Position? p)
        {
            return (p != null
                && p.Latitude == Latitude
                && p.Longitude == Longitude
                && p.Altitude == Altitude
                && p.DOP == DOP) 
                ? 0 : 1;
        }
    }
}
