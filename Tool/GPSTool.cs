
using UnityEngine;

public static class GPSTool
{
    public static Vector2 USCToGPS(Vector3 position, Vector2 localOrigin)
    {
        FindMetersPerLat(localOrigin.x,out float metersPerLat,out float metersPerLon);
        Vector2 geoLocation = new Vector2(0, 0);
        geoLocation.x = (localOrigin.x + (position.z) / metersPerLat); //Calc current lat
        geoLocation.y = (localOrigin.y + (position.x) / metersPerLon); //Calc current lon
        return geoLocation;
    }

    private static Vector3 GPStoUCS(Vector2 gps,Vector2 localOrigin)
    {
        FindMetersPerLat(localOrigin.x,out float metersPerLat,out float metersPerLon);
        float zPosition = metersPerLat * (gps.x - localOrigin.x); //Calc current lat
        float xPosition = metersPerLon * (gps.y - localOrigin.y); //Calc current lat
        return new Vector3((float)xPosition, 0, (float)zPosition);
    }

    private static void FindMetersPerLat(float lat,out float metersPerLat,out float metersPerLon) // Compute lengths of degrees
    {
        float m1 = 111132.92f;    // latitude calculation term 1
        float m2 = -559.82f;        // latitude calculation term 2
        float m3 = 1.175f;      // latitude calculation term 3
        float m4 = -0.0023f;        // latitude calculation term 4
        float p1 = 111412.84f;    // longitude calculation term 1
        float p2 = -93.5f;      // longitude calculation term 2
        float p3 = 0.118f;      // longitude calculation term 3

        lat = lat * Mathf.Deg2Rad;

        // Calculate the length of a degree of latitude and longitude in meters
        metersPerLat = m1 + (m2 * Mathf.Cos(2 * (float)lat)) + (m3 * Mathf.Cos(4 * (float)lat)) + (m4 * Mathf.Cos(6 * (float)lat));
        metersPerLon = (p1 * Mathf.Cos((float)lat)) + (p2 * Mathf.Cos(3 * (float)lat)) + (p3 * Mathf.Cos(5 * (float)lat));
    }

#if Vectord
    public static Vector2d USCToGPS(Vector3d position, Vector2d localOrigin)
    {
        FindMetersPerLat(localOrigin.x, out double metersPerLat, out double metersPerLon);
        Vector2d geoLocation = new Vector2d(0, 0);
        geoLocation.x = (localOrigin.x + (position.z) / metersPerLat); 
        geoLocation.y = (localOrigin.y + (position.x) / metersPerLon); 
        return geoLocation;
    }

    private static Vector3d GPStoUCS(Vector2d gps, Vector2d localOrigin)
    {
        FindMetersPerLat(localOrigin.x, out double metersPerLat, out double metersPerLon);
        double zPosition = metersPerLat * (gps.x - localOrigin.x); 
        double xPosition = metersPerLon * (gps.y - localOrigin.y);
        return new Vector3d(xPosition, 0, zPosition);
    }

    private static void FindMetersPerLat(double lat, out double metersPerLat, out double metersPerLon) // Compute lengths of degrees
    {
        double m1 = 111132.92;    // latitude calculation term 1
        double m2 = -559.82;        // latitude calculation term 2
        double m3 = 1.175;      // latitude calculation term 3
        double m4 = -0.0023;        // latitude calculation term 4
        double p1 = 111412.84;    // longitude calculation term 1
        double p2 = -93.5;      // longitude calculation term 2
        double p3 = 0.118;      // longitude calculation term 3

        lat = lat * Mathd.Deg2Rad;

        metersPerLat = m1 + (m2 * Mathd.Cos(2 * lat)) + (m3 * Mathd.Cos(4 * lat)) + (m4 * Mathd.Cos(6 * lat));
        metersPerLon = (p1 * Mathd.Cos(lat)) + (p2 * Mathd.Cos(3 * lat)) + (p3 * Mathd.Cos(5 * lat));
    }
#endif
}