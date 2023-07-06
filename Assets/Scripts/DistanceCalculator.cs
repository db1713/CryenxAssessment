using UnityEngine;

public class DistanceCalculator : MonoBehaviour
{
    public float CalculateDistance(float latA, float lonA, float latX, float lonX)
    {
        float earthRadius = 6371000f;

        // First i am converting latitude and longitude to radians
        float latARad = Mathf.Deg2Rad * latA;
        float lonARad = Mathf.Deg2Rad * lonA;
        float latXRad = Mathf.Deg2Rad * latX;
        float lonXRad = Mathf.Deg2Rad * lonX;

        // Calculating the difference in latitude and longitude
        float deltaLat = latXRad - latARad;
        float deltaLon = lonXRad - lonARad;

        // This is the haversine calculation used to calculate distance between two sets of coordinates in meters or kilometers, this is how i can trigger the prompt based on proximity to geolocation
        float a = Mathf.Sin(deltaLat / 2f) * Mathf.Sin(deltaLat / 2f) +
                  Mathf.Cos(latARad) * Mathf.Cos(latXRad) *
                  Mathf.Sin(deltaLon / 2f) * Mathf.Sin(deltaLon / 2f);
        float c = 2f * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1f - a));
        float distance = earthRadius * c;

        return distance;
    }
}
